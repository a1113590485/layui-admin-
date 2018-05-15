using Bootstrap.Entity.Models.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bootstrap.Web.Areas.Manage.Views.Menu.Dto
{
    /// <summary>
    /// 添加/修改菜单数据模型
    /// </summary>
    public class AddOrEditMenuModels
    {
        /// <summary>
        /// 父级菜单下拉列表
        /// </summary>
        public List<ParentMenuList> ParentMenuList { get; set; }
        /// <summary>
        /// 菜单详情
        /// </summary>
        public NavigationMenu MenuInfo { get; set; }
    }
}