using Bootstrap.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrap.Entity.Models.System
{
    /// <summary>
    /// 用户-权限关系表
    /// </summary>
    public class UserPermissionRelation:EntityDto<int>
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionName { get; set; }
    }
}
