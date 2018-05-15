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
    /// 权限
    /// </summary>
    public class Permission:EntityDto<int>
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public virtual string ModuleName { get; set; }
        /// <summary>
        /// 简称-唯一
        /// </summary>
        public virtual string ShortName { get; set; }
        /// <summary>
        /// 父级权限
        /// </summary>
        public virtual int? ParentPermissionId { get; set; }
        [ForeignKey("ParentPermissionId")]
        public virtual ICollection<Permission> ChildPermissionList { get; set; }
    }
}
