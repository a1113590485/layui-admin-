using Bootstrap.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootstrap.Entity.Models.System
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class User:EntityDto<int>
    {
        //用户名最小长度
        public const int MinUserNameLength = 3;
        //用户名最大长度
        public const int MaxUserNameLength = 8;
        public User()
        {
            CreationTime = DateTime.Now;
            Sex = Gender.未知;
        }
        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(MaxUserNameLength)]
        [MinLength(MinUserNameLength)]
        public virtual string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public virtual string PassWord { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public virtual string NickName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual Gender Sex { get; set; }
        /// <summary>
        /// 用户状态
        /// </summary>
        public virtual Status UserStatus { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }
        /// <summary>
        /// 上一次登录时间
        /// </summary>
        public virtual DateTime? LastLoginTime { get; set; }


        /// <summary>
        /// 性别
        /// </summary>
        public enum Gender
        {
            未知 = 0,
            男 = 1,
            女 = 2
        }
        public enum Status
        {
            未激活 = 1,
            正常 = 2,
            已冻结 = 3
        }
    }
}
