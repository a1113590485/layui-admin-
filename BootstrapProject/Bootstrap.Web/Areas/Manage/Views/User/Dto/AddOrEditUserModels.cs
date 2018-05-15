using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bootstrap.Web.Areas.Manage.Views.User.Dto
{
    /// <summary>
    /// 添加/编辑用户视图模型
    /// </summary>
    public class AddOrEditUserModels
    {
        public int Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 用户状态
        /// </summary>
        public string UserStatus { get; set; }   
        /// <summary>
        /// 用户角色
        /// </summary>
        public List<int> UserRoles { get; set; }
        /// <summary>
        /// 用户性别列表--下拉框
        /// </summary>
        public List<SelectListItem> UserGenderList { get; set; }
        /// <summary>
        /// 角色列表--下拉框
        /// </summary>
        public List<Entity.Models.System.Role> RoleList { get; set; }
    }
}