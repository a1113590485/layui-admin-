using Bootstrap.Entity.Base;
using Bootstrap.Entity.Models;
using Bootstrap.Entity.Models.System;
using Bootstrap.Entity.Repository;
using Bootstrap.Web.Areas.Manage.Dto;
using Bootstrap.Web.Areas.Manage.Views.Login.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bootstrap.Web.Areas.Manage.Controllers
{
    public class LoginController : Controller
    {
        private readonly CommonModel _commonModel = new CommonModel();
        public User CurrentUser
        {
            get
            {
                var user = HttpContext.Session["CurrentUser"];
                if (user == null) return new User();
                var userInfo = JsonConvert.DeserializeObject<User>(user.ToString());
                return userInfo;
            }
            set
            {
                HttpContext.Session["CurrentUser"] = value;
            }
        }

        // GET: Manage/Login
        public ActionResult Index()
        {
            //若当前用户登录了，直接跳转至Home页面
            if (CurrentUser.Id > 0)
            {
                Response.Redirect("/Manage/Home/Index");
            }
            return View();
        }

        /// <summary>
        /// 用户注册页面
        /// </summary>
        /// <returns></returns>
        public ActionResult RegisterView()
        {
            return View();
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult LoginIn(LoginInput input)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //密码MD5加密
                    input.PassWord = CommonMethods.MD5(input.PassWord);
                    var user = _commonModel.UserRepository.FirstOrDefault(o => o.UserName == input.UserName && o.PassWord == input.PassWord);
                    if (user != null)
                    {
                        //清除所有session
                        HttpContext.Session.RemoveAll();
                        HttpContext.Session["CurrentUser"] = JsonConvert.SerializeObject(user);
                        return Json(new PublicOutput { Success = true, Msg = "登陆成功" },JsonRequestBehavior.AllowGet);
                    }
                    return Json(new PublicOutput { Success = false, Msg = "账号或密码错误" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json(new PublicOutput { Success = false, Msg = "系统内部错误" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new PublicOutput { Success = false, Msg = "系统内部错误" }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginOut()
        {
            HttpContext.Session.RemoveAll();
            return Json(new PublicOutput { Success = true, Msg = "退出成功" });
        }
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> Register(RegisterInput input)
        {
            try
            {
                //判断该手机号是否已经注册过
                var query = _commonModel.UserRepository.GetAllAsNoTracking().Where(o => o.UserName.Equals(input.PhoneNumber, StringComparison.InvariantCultureIgnoreCase) || o.PhoneNumber.Equals(input.PhoneNumber, StringComparison.InvariantCultureIgnoreCase)).ToList();
                if (query.Count() > 0)
                {
                    return Json(new PublicOutput { Success = false, Msg = "该手机号已注册过" });
                }
                var newUser = new User
                {
                    NickName = input.NickName,
                    UserName = input.PhoneNumber,
                    PhoneNumber = input.PhoneNumber,
                    PassWord = CommonMethods.MD5(input.PassWord),
                    UserStatus = Entity.Models.System.User.Status.正常
                };
                await _commonModel.UserRepository.InsertAsync(newUser);
                return Json(new PublicOutput { Success = true, Msg = "注册成功" });
            }
            catch (Exception ex)
            {
                return Json(new PublicOutput { Success = false, Msg = "系统内部错误" });
            }
        }
    }
}