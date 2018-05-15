using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bootstrap.Web.Areas.Manage.Views.User.Dto
{
    /// <summary>
    /// 用户 - 角色 关系
    /// </summary>
    public class UserAndRoleRelation
    {
        public Bootstrap.Entity.Models.System.User User { get; set; }
        public Bootstrap.Entity.Models.System.UserRoleRelation UserRoleRelation { get; set; }
    }
}