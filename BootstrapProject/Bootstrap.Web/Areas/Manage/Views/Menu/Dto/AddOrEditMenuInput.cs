using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bootstrap.Web.Areas.Manage.Views.Menu.Dto
{
    public class AddOrEditMenuInput
    {
        public int Id { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// 菜单简称
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// 跳转Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 父级菜单Id
        /// </summary>
        public int? ParentMenuId { get; set; }
    }
}