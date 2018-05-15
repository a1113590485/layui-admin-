using AutoMapper;
using Bootstrap.Entity.Base;
using Bootstrap.Entity.Models.System;
using Bootstrap.Entity.Repository;
using Bootstrap.Web.Areas.Manage.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
using Bootstrap.Web.Areas.Manage.Views.Role.Dto;
using System.ComponentModel;

namespace Bootstrap.Web.Areas.Manage.Controllers
{
    /// <summary>
    /// 角色管理
    /// </summary>
    [Description("角色管理控制器")]
    public class RoleController : BaseController
    {
        private readonly CommonModel _commonModel = new CommonModel();

        // GET: Manage/Role
        [Description("角色管理页面")]
        public ActionResult Index()
        {
            ViewBag.CurrentUser = CurrentUser;
            return View();
        }
        /// <summary>
        /// 新增/编辑角色分布视图页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Description("新增/编辑角色页面")]
        public PartialViewResult AddOrEidtRoleView(int? id)
        {
            var model = new Role();
            if (id.HasValue)
            {
                model = _commonModel.RoleRepository.Get(id.Value);
            }
            return PartialView(model);
        }

        [Description("角色权限分配页面")]
        public PartialViewResult ChangePermissionView(int id)
        {            
            return PartialView(id);

        }

        /// <summary>
        /// 新增/编辑角色
        /// </summary>
        /// <returns></returns>
        [Description("新增/编辑角色")]
        public async Task<JsonResult> AddOrEditRole(AddOrEditRoleInput input)
        {
            if (input.Id>0)
            {
                try
                {
                    var query = _commonModel.RoleRepository.Get(input.Id);
                    query.RoleName = input.RoleName;
                    query.RoleDesc = input.RoleDesc;
                    await _commonModel.RoleRepository.UpdateAsync(query);
                    return Json(new PublicOutput { Success = true, Msg = "角色修改成功" });
                }
                catch (Exception)
                {
                    return Json(new PublicOutput { Success = false, Msg = "角色修改失败" });
                }
            }
            else
            {
                try
                {
                    var newRole = new Role { RoleDesc = input.RoleDesc, RoleName = input.RoleName };
                    await _commonModel.RoleRepository.InsertAsync(newRole);
                    return Json(new PublicOutput { Success = true, Msg = "角色新增成功" });
                }
                catch (Exception)
                {
                    return Json(new PublicOutput { Success = false, Msg = "角色新增失败" });
                }
            }
        }
        /// <summary>
        /// 批量删除角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Description("批量删除角色")]
        public async Task<JsonResult> BatchDeleteRole(string Ids)
        {
            try
            {
                string[] idsList = Ids.Split(',');
                await _commonModel.RoleRepository.DeleteAsync(o => idsList.Contains(o.Id.ToString()));
                return Json(new PublicOutput { Success = true, Msg = "批量删除成功" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new PublicOutput { Success = false, Msg = "批量删除失败" }, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        [Description("获取角色列表")]
        public JsonResult GetRoleList()
        {
            var query = _commonModel.RoleRepository.GetAll().ToList();
            var data = query.Select(o => new RoleListOutput { Id = o.Id, RoleName = o.RoleName, RoleDesc = o.RoleDesc });
            var output = new PublicTableOutput<IEnumerable<RoleListOutput>>
            {
                code = 0,
                count = data.Count(),
                data = data,
                msg = ""
            };
            return Json(output, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Description("删除角色")]
        public async Task<JsonResult> DeleteRole(int Id)
        {
            try
            {
                var query = _commonModel.RoleRepository.Get(Id);
                await _commonModel.RoleRepository.DeleteAsync(query);
                return Json(new PublicOutput { Success = true, Msg = "角色删除成功" });
            }
            catch (Exception)
            {
                return Json(new PublicOutput { Success = false, Msg = "角色删除失败" });
            }
        }

        /// <summary>
        /// 根据角色获取权限列表
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [Description("获取权限列表")]
        public JsonResult GetRolePermissionList(int RoleId)
        {
            var query = _commonModel.RolePermissionRelationRepository.GetAll().Select(o=>o.PermissionName).ToList();
            var permissionQuery = _commonModel.PermissionRepository.GetAllAsNoTracking();
            var permissionList = permissionQuery.Select(o=>new PermissionListOutput
            {
                Id = o.Id,
                ShortName = o.ShortName,
                ModuleName = o.ModuleName
            }).ToList();
            permissionList.ForEach(o=>o.ChildrenList= permissionList.Where(a=>a.ParentPermissionId==o.Id).ToList());
            permissionList = permissionList.Where(o=>!o.ParentPermissionId.HasValue).ToList();
            return Json(permissionList, JsonRequestBehavior.AllowGet);
        }
        [Description("保存角色权限")]
        public async Task<JsonResult> SaveRolePermission(SaveRolePermissionInput input)
        {
            try
            {
                //先删除原先的所有关系数据
                await _commonModel.RolePermissionRelationRepository.DeleteAsync(o => o.RoleId == input.RoleId);
                //添加关系数据
                foreach (var item in input.PermissionName)
                {
                    await _commonModel.RolePermissionRelationRepository.InsertAsync(new RolePermissionRelation { RoleId = input.RoleId, PermissionName = item });
                }
                return Json(new PublicOutput { Success = true, Msg = "角色权限保存成功" });
            }
            catch (Exception)
            {
                return Json(new PublicOutput { Success = false, Msg = "角色权限保存失败" });
            }
        }


        
    }
}