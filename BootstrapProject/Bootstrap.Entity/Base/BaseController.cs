using Bootstrap.Entity.Models;
using Bootstrap.Entity.Models.System;
using Bootstrap.Entity.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bootstrap.Entity.Base
{
    [ValidateLogin]
    public class BaseController:Controller
    {
        private readonly CommonModel _commonModel = new CommonModel();

        public BaseController()
        {
            //菜单列表
            var query = _commonModel.NavigationMenuRepository.GetAllAsNoTracking().Where(o=>!o.ParentMenuId.HasValue).ToList();
           
            ViewData["_layoutMenuList"] = query;
        }

        #region 当前用户信息

        /// <summary>
        /// 获取当前登陆人信息
        /// </summary>
        public User CurrentUser
        {
            get
            {
                try
                {
                    var currentUser = HttpContext.Session["CurrentUser"];
                    if (currentUser == null) return new User();
                    var userInfo = JsonConvert.DeserializeObject<User>(currentUser.ToString());
                    if (userInfo != null)
                    {
                        //用户权限列表
                        var userPermissionList = _commonModel.UserPermissionRelationRepository.GetAllAsNoTracking().Where(o => o.UserId == userInfo.Id).ToList();

                        ViewData["_layoutPermissionList"] = userPermissionList.Select(o => o.PermissionName).ToList();

                        return userInfo;
                    }
                    else
                    {
                        return new User();
                    }
                }
                catch (Exception)
                {
                    return new User();
                }
            }
            set
            {
                HttpContext.Session["CurrentUser"] = value;
            }
        }
        #endregion
        
    }
}
