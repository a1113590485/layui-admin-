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
            var userListOutput = userList.Select(u=>new UserListOutput
            {
                Id = u.Id,
                CreationTime = u.CreationTime,
                LastLoginTime = u.LastLoginTime,
                NickName = u.NickName,
                Sex = Enum.GetName(typeof(User.Gender), u.Sex),
                UserName = u.UserName,
                UserStatus = Enum.GetName(typeof(User.Status), u.UserStatus)
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
        
    }
}