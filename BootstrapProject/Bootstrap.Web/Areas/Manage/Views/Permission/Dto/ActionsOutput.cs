using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bootstrap.Web.Areas.Manage.Views.Permission.Dto
{
    public class ActionsOutput
    {
        /// <summary>
        /// 请求地址
        /// </summary>
        public string ActionLink { get; set; }
        /// <summary>
        /// 接口名称
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 是否选中-即代表该角色是否具有该权限
        /// </summary>
        public bool IsCheck { get; set; }
    }
}