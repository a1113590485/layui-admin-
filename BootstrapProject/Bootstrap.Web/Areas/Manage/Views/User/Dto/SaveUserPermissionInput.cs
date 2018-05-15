using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bootstrap.Web.Areas.Manage.Views.User.Dto
{
    public class SaveUserPermissionInput
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 权限名
        /// </summary>
        public List<string> PermissionName { get; set; }
    }
}