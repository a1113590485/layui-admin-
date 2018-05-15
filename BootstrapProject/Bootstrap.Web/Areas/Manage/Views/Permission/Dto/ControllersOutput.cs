using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bootstrap.Web.Areas.Manage.Views.Permission.Dto
{
    public class ControllersOutput
    {
        /// <summary>
        /// 控制器名称
        /// </summary>
        public string ControllerName { get; set; }
        /// <summary>
        /// Controller名称,通过反射Desction属性获得
        /// </summary>
        public string ControllerDesction { get; set; }
        /// <summary>
        /// 是否选中-即代表该角色是否具有该权限
        /// </summary>
        public bool IsCheck { get; set; }
        /// <summary>
        /// Action列表
        /// </summary>
        public List<ActionsOutput> ActionList { get; set; }
    }
}