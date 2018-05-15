using Bootstrap.Entity.Repository;
using Bootstrap.Web.Areas.Manage.Views.Permission.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bootstrap.Web.Areas.Manage.Controllers
{
    [Description("权限控制器")]
    public class PermissionController : Controller
    {
        private readonly CommonModel _commonModel = new CommonModel();

        // GET: Manage/Permission
        
        [Description("获取权限列表-根据角色")]
        public JsonResult GetPermissionListByRole(int roleId)
        {
            //获取角色权限名称列表
            var rolePermissionList = _commonModel.RolePermissionRelationRepository.GetAll().Where(o => o.RoleId == roleId).Select(o => o.PermissionName).ToList();

            //控制器列表
            List<ControllersOutput> controls = new List<ControllersOutput>();

            var asm = System.Reflection.Assembly.Load("Bootstrap.Web");
            System.Collections.Generic.List<Type> typeList = new List<Type>();
            var types = asm.GetTypes();
            foreach (Type type in types)
            {
                string s = type.FullName.ToLower();
                if (s.EndsWith("controller") && type.BaseType.Name == "BaseController")
                    typeList.Add(type);
            }
            //循环controller
            foreach (Type type in typeList)
            {
                ControllersOutput newControl = new ControllersOutput();

                System.Reflection.MemberInfo[] members = type.FindMembers(
                    System.Reflection.MemberTypes.Method,
                    System.Reflection.BindingFlags.Public |
                    System.Reflection.BindingFlags.Static |
                    System.Reflection.BindingFlags.NonPublic |        //【位屏蔽】 
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.DeclaredOnly,
                    Type.FilterName, "*"
                );
                string controller = type.Name.Replace("Controller", "");
                //完整路由
                var fullRoute = "/" + type.FullName.Split('.')[3] + "/" + controller;

                //反射Description属性，作为菜单名称 
                object[] description = type.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (description != null && description.Length > 0)
                {
                    foreach (var item in description)
                    {
                        DescriptionAttribute da = item as DescriptionAttribute;
                        newControl.ControllerDesction = da.Description;
                        break;
                    }
                }
                else
                {
                    newControl.ControllerDesction = fullRoute;
                }
                newControl.ControllerName = fullRoute;
                if (rolePermissionList.Contains(fullRoute))
                {
                    newControl.IsCheck = true;
                }
                else
                {
                    newControl.IsCheck = false;
                }
                //手动构建controllersoutput下的ActionList,直接循环add会导致失败
                var actionList = new List<ActionsOutput>();
                //循环action
                foreach (var m in members)
                {
                    ActionsOutput newAction = new ActionsOutput();
                    object[] deser = m.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (description != null && description.Length > 0)
                    {
                        foreach (var item in deser)
                        {
                            DescriptionAttribute da = item as DescriptionAttribute;
                            newAction.ActionName = da.Description;
                            break;
                        }
                    }
                    else
                    {
                        newAction.ActionName = m.Name;
                    }
                    newAction.ActionLink = fullRoute + "/" + m.Name;
                    if (rolePermissionList.Contains(newAction.ActionLink))
                    {
                        newAction.IsCheck = true;
                    }
                    else
                    {
                        newControl.IsCheck = false;
                    }
                    actionList.Add(newAction);
                }
                newControl.ActionList = actionList;
                controls.Add(newControl);
            }
            return Json(controls);
        }

        [Description("获取权限列表-根据用户")]
        public JsonResult GetPermissionListByUser(int userId)
        {
            //获取用户权限名称列表
            var userPermissionList = _commonModel.UserPermissionRelationRepository.GetAll().Where(o => o.UserId == userId).Select(o => o.PermissionName).ToList();

            //控制器列表
            List<ControllersOutput> controls = new List<ControllersOutput>();

            var asm = System.Reflection.Assembly.Load("Bootstrap.Web");
            System.Collections.Generic.List<Type> typeList = new List<Type>();
            var types = asm.GetTypes();
            foreach (Type type in types)
            {
                string s = type.FullName.ToLower();
                if (s.EndsWith("controller") && type.BaseType.Name == "BaseController")
                    typeList.Add(type);
            }
            //循环controller
            foreach (Type type in typeList)
            {
                ControllersOutput newControl = new ControllersOutput();

                System.Reflection.MemberInfo[] members = type.FindMembers(
                    System.Reflection.MemberTypes.Method,
                    System.Reflection.BindingFlags.Public |
                    System.Reflection.BindingFlags.Static |
                    System.Reflection.BindingFlags.NonPublic |        //【位屏蔽】 
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.DeclaredOnly,
                    Type.FilterName, "*"
                );
                string controller = type.Name.Replace("Controller", "");
                //完整路由
                var fullRoute = "/" + type.FullName.Split('.')[3] + "/" + controller;

                //反射Description属性，作为菜单名称 
                object[] description = type.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (description != null && description.Length > 0)
                {
                    foreach (var item in description)
                    {
                        DescriptionAttribute da = item as DescriptionAttribute;
                        newControl.ControllerDesction = da.Description;
                        break;
                    }
                }
                else
                {
                    newControl.ControllerDesction = fullRoute;
                }
                newControl.ControllerName = fullRoute;
                if (userPermissionList.Contains(fullRoute))
                {
                    newControl.IsCheck = true;
                }
                else
                {
                    newControl.IsCheck = false;
                }
                //手动构建controllersoutput下的ActionList,直接循环add会导致失败
                var actionList = new List<ActionsOutput>();
                //循环action
                foreach (var m in members)
                {
                    ActionsOutput newAction = new ActionsOutput();
                    object[] deser = m.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (description != null && description.Length > 0)
                    {
                        foreach (var item in deser)
                        {
                            DescriptionAttribute da = item as DescriptionAttribute;
                            newAction.ActionName = da.Description;
                            break;
                        }
                    }
                    else
                    {
                        newAction.ActionName = m.Name;
                    }
                    newAction.ActionLink = fullRoute + "/" + m.Name;
                    if (userPermissionList.Contains(newAction.ActionLink))
                    {
                        newAction.IsCheck = true;
                    }
                    else
                    {
                        newControl.IsCheck = false;
                    }
                    actionList.Add(newAction);
                }
                newControl.ActionList = actionList;
                controls.Add(newControl);
            }
            return Json(controls);
        }
    }
}