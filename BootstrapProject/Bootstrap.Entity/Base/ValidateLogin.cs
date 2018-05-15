using Bootstrap.Entity.Models.System;
using Bootstrap.Entity.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bootstrap.Entity.Base
{
    /// <summary>
    /// 授权验证类
    /// </summary>
    public class ValidateLogin: ActionFilterAttribute
    {
        private readonly CommonModel _commonModel = new CommonModel();
        /// <summary>
        /// 在控制器之前执行的方法
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                //若当前session中判断currentUser不存在，则会话过期
                if (filterContext.HttpContext.Session["CurrentUser"] == null)
                {
                    filterContext.HttpContext.Response.Write("<script>alert('当前会话已过期,请重新登陆');window.location.href='../../Manage/Login/Index';</script>");
                    filterContext.HttpContext.Response.End();
                }
                else
                {
                    var currentUser = JsonConvert.DeserializeObject<User>(filterContext.HttpContext.Session["CurrentUser"].ToString());
                    //用户权限判断
                    var userPermissionList = _commonModel.UserPermissionRelationRepository.GetAllAsNoTracking().Where(o => o.UserId == currentUser.Id).Select(o => o.PermissionName).ToList();
                    //获取当前控制器及action
                    var currentRoute = "/" + filterContext.ActionDescriptor.ControllerDescriptor.ControllerType.FullName.Split('.')[3] + "/" + filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "/" + filterContext.ActionDescriptor.ActionName;
                    if (!userPermissionList.Contains(currentRoute))
                    {
                        filterContext.HttpContext.Response.Write("<script>window.location.href='../../Manage/Error/Index';</script>");
                        filterContext.HttpContext.Response.End();
                    }
                }
            }
            catch (Exception)
            {
                filterContext.HttpContext.Response.Write("<script>alert('当前会话已过期,请重新登陆');window.location.href='../../Manage/Login/Index';</script>");
                filterContext.HttpContext.Response.End();
            }
        }
    }
}
