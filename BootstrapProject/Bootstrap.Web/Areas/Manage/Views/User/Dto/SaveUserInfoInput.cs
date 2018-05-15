using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bootstrap.Web.Areas.Manage.Views.User.Dto
{
    /// <summary>
    /// 保存用户 input
    /// </summary>
    public class SaveUserInfoInput
    {
        public int Id { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 用户性别
        /// </summary>
        public Entity.Models.System.User.Gender Sex { get; set; }
        /// <summary>
        /// 用户角色
        /// </summary>
        public List<int> UserRoles { get; set; }
    }
}