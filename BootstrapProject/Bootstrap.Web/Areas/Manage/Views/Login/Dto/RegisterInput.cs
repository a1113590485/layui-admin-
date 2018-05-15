using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bootstrap.Web.Areas.Manage.Views.Login.Dto
{
    /// <summary>
    /// 用户注册input
    /// </summary>
    public class RegisterInput
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
    }
}