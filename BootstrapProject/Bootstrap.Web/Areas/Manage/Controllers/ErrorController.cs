using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bootstrap.Web.Areas.Manage.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Manage/Error
        /// <summary>
        /// 错误控制器
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}