using Bootstrap.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrap.Entity.Models.System
{
    /// <summary>
    /// 角色权限关联表
    /// </summary>
    public class RolePermissionRelation:EntityDto<int>
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionName { get; set; }
    }
}
