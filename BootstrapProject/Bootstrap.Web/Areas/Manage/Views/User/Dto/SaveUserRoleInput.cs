using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bootstrap.Web.Areas.Manage.Views.User.Dto
{
    /// <summary>
    /// 保存用户角色Input
    /// </summary>
    public class SaveUserRoleInput
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 角色列表
        /// </summary>
        public List<int> RoleIds { get; set; }
    }
}