using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bootstrap.Web.Areas.Manage.Views.Role.Dto
{
    /// <summary>
    /// 保存角色权限input
    /// </summary>
    public class SaveRolePermissionInput
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 权限名
        /// </summary>
        public List<string> PermissionName { get; set; }
    }
}