using Bootstrap.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrap.Entity.Models.System
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class Role : EntityDto<int>
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public virtual string RoleName { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        public virtual string RoleDesc { get; set; }
    }
}
