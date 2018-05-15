using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bootstrap.Web.Areas.Manage.Views.Role.Dto
{
    /// <summary>
    /// 权限列表output 
    /// </summary>
    public class PermissionListOutput
    {
        public int Id { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        /// guid
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// 父级权限Id
        /// </summary>
        public int? ParentPermissionId { get; set; }
        public List<PermissionListOutput> ChildrenList { get; set; }
    }
}