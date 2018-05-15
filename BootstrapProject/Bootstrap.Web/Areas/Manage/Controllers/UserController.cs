using Bootstrap.Entity.Base;
using Bootstrap.Entity.Models.System;
using Bootstrap.Entity.Repository;
using Bootstrap.Web.Areas.Manage.Dto;
using Bootstrap.Web.Areas.Manage.Views.User.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bootstrap.Web.Areas.Manage.Controllers
{
    [Description("用户管理控制器")]
    public class UserController : BaseController
    {
        private readonly CommonModel _commonModel = new CommonModel();

        // GET: Manage/User
        [Description("用户管理页面")]
        public ActionResult Index()
        {
            ViewBag.CurrentUser = CurrentUser;
            return View();
        }
        [Description("用户权限分配页面")]
        public ActionResult ChangePermissionView(int id)
        {
            return PartialView(id);
        }
        [Description("新增/编辑用户页面")]
        public ActionResult AddOrEditUserView(int? id)
        {
            var user = new User();
            List<int> userRoles = null;
            if (id.HasValue)
            {
                user = _commonModel.UserRepository.Get(id.Value);
                userRoles = _commonModel.UserRoleRelationRepository.GetAll().Where(o => o.UserId == user.Id).Select(o=>o.RoleId).ToList();
            }
            //性别下拉框赋值
            var userGenderList = new List<SelectListItem>();
            foreach (var item in Enum.GetValues(typeof(User.Gender)))
            {
                var listitem = new SelectListItem
                {
                    Value = item.ToString(),
                    Text = Enum.GetName(typeof(User.Gender), item),
                    Selected = (int)user.Sex == (int)item ? true : false
                };
                userGenderList.Add(listitem);
            }
            var roleList = _commonModel.RoleRepository.GetAllAsNoTracking().ToList();
            //构建返回的model
            var model = new AddOrEditUserModels()
            {
                Id = user.Id,
                NickName = user.NickName,
                UserName = user.UserName,
                UserRoles = userRoles,
                UserStatus = Enum.GetName(typeof(User.Status),user.UserStatus),
                UserGenderList = userGenderList,
                RoleList= roleList
            };
            return PartialView(model);
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="limit">每页大小</param>
        /// <returns></returns>
        [Description("获取用户列表")]
        public JsonResult GetUserList(int page, int limit)
        {
            //调用分页数据
            var userList = _commonModel.UserRepository
                .GetItemsByPage(limit, page,u=>u.CreationTime).ToList();
            //获取所有用户角色关系
            var allUserRole = _commonModel.UserRoleRelationRepository.GetAllAsNoTracking();
            //获取所有角色
            var allRole = _commonModel.RoleRepository.GetAllAsNoTracking();
            var userListOutput = userList.Select(u =>
            {
                var dto = new UserListOutput
                {
                    Id = u.Id,
                    CreationTime = u.CreationTime,
                    LastLoginTime = u.LastLoginTime,
                    NickName = u.NickName,
                    Sex = Enum.GetName(typeof(User.Gender), u.Sex),
                    UserName = u.UserName,
                    UserStatus = Enum.GetName(typeof(User.Status), u.UserStatus),
                };
                var userRoleList = allUserRole.Where(o => o.UserId == dto.Id).Select(o => o.RoleId).ToList();
                var roles = allRole.Where(o => userRoleList.Contains(o.Id)).Select(o=>o.RoleName).ToList();
                dto.UserRoles = string.Join(",", roles);
                return dto;
            }).ToList();

            var output = new PublicTableOutput<List<UserListOutput>>
            {
                code = 0,
                count = userListOutput.Count(),
                msg = "",
                data = userListOutput
            };
            return Json(output,JsonRequestBehavior.AllowGet);
        }
        [Description("保存用户权限")]
        public async Task<JsonResult> SaveUserPermission(SaveUserPermissionInput input)
        {
            try
            {
                //先删除原先的所有关系数据
                await _commonModel.UserPermissionRelationRepository.DeleteAsync(o => o.UserId == input.UserId);
                //添加关系数据
                foreach (var item in input.PermissionName)
                {
                    await _commonModel.UserPermissionRelationRepository.InsertAsync(new UserPermissionRelation { UserId = input.UserId, PermissionName = item });
                }
                return Json(new PublicOutput { Success = true, Msg = "用户权限保存成功" });
            }
            catch (Exception)
            {
                return Json(new PublicOutput { Success = false, Msg = "用户权限保存失败" });
            }
        }
        [Description("保存用户详情")]
        public async Task<JsonResult> SaveUserInfo(SaveUserInfoInput input)
        {
            try
            {
                var user = _commonModel.UserRepository.Get(input.Id);
                user.NickName = input.NickName;
                user.Sex = input.Sex;
                await _commonModel.UserRepository.UpdateAsync(user);

                //删除原先的用户-角色关系数据
                await _commonModel.UserRoleRelationRepository.DeleteAsync(o => o.UserId == input.Id);
                //添加关系数据
                foreach (var item in input.UserRoles)
                {
                    await _commonModel.UserRoleRelationRepository.InsertAsync(new UserRoleRelation { RoleId = item, UserId = input.Id });
                }
                //删除原先的用户-权限关系数据
                await _commonModel.UserPermissionRelationRepository.DeleteAsync(o => o.UserId == input.Id);
                //添加关系数据
                //1.获取所有权限-去重复
                var permissionList = _commonModel.RolePermissionRelationRepository.GetAllAsNoTracking().Where(o => input.UserRoles.Contains(o.RoleId)).Distinct().Select(o => o.PermissionName).ToList();
                //2.建立联系
                foreach (var item in permissionList)
                {
                    await _commonModel.UserPermissionRelationRepository.InsertAsync(new UserPermissionRelation { UserId = input.Id, PermissionName = item });
                }

                return Json(new PublicOutput { Success = true, Msg = "用户信息保存成功" });
            }
            catch (Exception)
            {
                return Json(new PublicOutput { Success = false, Msg = "用户信息保存失败" });
            }
        }
    }
}