using Bootstrap.Entity.Base;
using Bootstrap.Entity.Models;
using Bootstrap.Entity.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bootstrap.Web.Areas.Manage.Controllers
{
    [Description("Home控制器")]
    public class HomeController : BaseController
    {
        private readonly CommonModel _commonModel = new CommonModel();

        // GET: Manage/Home
        [Description("Home控制器页面")]
        public ActionResult Index()
        {
            ViewBag.CurrentUser = CurrentUser;
            return View();
        }
    }
}