using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bootstrap.Web.Areas.Manage.Views.Menu.Dto
{
    /// <summary>
    /// 父级菜单列表-下拉框用
    /// </summary>
    public class ParentMenuList
    {
        public int Id { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }
    }
}