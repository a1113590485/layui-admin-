using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bootstrap.Web.Areas.Manage.Views.Login.Dto
{
    public class LoginInput
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [DisplayName("用户名")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("密码")]
        public string PassWord { get; set; }
    }
}