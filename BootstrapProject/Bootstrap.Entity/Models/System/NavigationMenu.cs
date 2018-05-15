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
    /// 导航菜单
    /// </summary>
    public class NavigationMenu:EntityDto<int>
    {
        /// <summary>
        /// 简称-唯一标识
        /// </summary>
        public virtual string ShortName { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public virtual string MenuName { get; set; }
        /// <summary>
        /// 跳转Url
        /// </summary>
        public virtual string Url { get; set; }

        /// <summary>
        /// 父级菜单Id
        /// </summary>
        public virtual int? ParentMenuId { get; set; }
        [ForeignKey("ParentMenuId")]
        public virtual ICollection<NavigationMenu> ChildMenuList { get; set; }
    }
}
