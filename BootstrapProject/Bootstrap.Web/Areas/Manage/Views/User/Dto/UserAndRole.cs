using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bootstrap.Web.Areas.Manage.Views.User.Dto
{
    /// <summary>
    /// 用户-角色
    /// </summary>
    public class UserAndRole:UserAndRoleRelation
    {
        public Bootstrap.Entity.Models.System.Role Role{ get; set; }
    }
}