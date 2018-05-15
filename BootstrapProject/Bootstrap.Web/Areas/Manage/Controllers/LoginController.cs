using Bootstrap.Entity.Base;
using Bootstrap.Entity.Models;
using Bootstrap.Entity.Models.System;
using Bootstrap.Entity.Repository;
using Bootstrap.Web.Areas.Manage.Views.Login.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
                User user = HttpContext.Session["CurrentUser"] as User;
                if (user != null)
                {
                    return user;
                }else
                {
                    return new User();
                }
            }
            set
            {
                HttpContext.Session["CurrentUser"] = value;
            }
        }

        // GET: Manage/Login
        public ActionResult Index()
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
                        return Json(new { Success = true, Msg = "登陆成功" },JsonRequestBehavior.AllowGet);
                    }
                    return Json(new { Success = false, Msg = "账号或密码错误" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json(new { Success = false, Msg = "系统内部错误" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = false, Msg = "系统内部错误" }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginOut()
        {
            try
            {
                HttpContext.Session.RemoveAll();
                return Redirect("/Manage/Login/Index");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}