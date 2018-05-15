using Bootstrap.Entity.Base;
using Bootstrap.Entity.Models.System;
using Bootstrap.Entity.Repository;
using Bootstrap.Web.Areas.Manage.Dto;
using Bootstrap.Web.Areas.Manage.Views.Menu.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bootstrap.Web.Areas.Manage.Controllers
{
    /// <summary>
    /// 菜单管理控制器
    /// </summary>
    [Description("菜单控制器")]
    public class MenuController : BaseController
    {
        private readonly CommonModel _commonModel = new CommonModel();
        // GET: Manage/Menu
        [Description("菜单页面")]
        public ActionResult Index()
        {
            ViewBag.CurrentUser = CurrentUser;
            return View();
        }
        [Description("添加/修改页面")]
        public PartialViewResult AddOrEidtMenuView(int? Id)
        {
            var parentMenuList = _commonModel.NavigationMenuRepository.GetAll().Where(o => !o.ParentMenuId.HasValue)
                .Select(o=>new ParentMenuList
                {
                    Id = o.Id,
                    MenuName = o.MenuName
                }).ToList();
            var menuInfo = new NavigationMenu();
            if (Id.HasValue)
            {
                menuInfo = _commonModel.NavigationMenuRepository.Get(Id.Value);
            }
            AddOrEditMenuModels model = new AddOrEditMenuModels
            {
                MenuInfo = menuInfo,
                ParentMenuList = parentMenuList
            };
            return PartialView(model);
        }
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        [Description("获取菜单树")]
        public JsonResult GetMenuTree()
        {
            var query = _commonModel.NavigationMenuRepository.GetAllAsNoTracking().Where(o=>!o.ParentMenuId.HasValue).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加.修改菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Description("添加/修改菜单")]
        public async Task<JsonResult> AddOrEidtMenu(AddOrEditMenuInput input)
        {
            //修改
            if (input.Id>0)
            {
                try
                {
                    //判断重复
                    var menuEntity = _commonModel.NavigationMenuRepository.GetAll().Where(o => o.ShortName == input.ShortName && o.Id != input.Id);
                    if(menuEntity.Count()>0) return Json(new PublicOutput { Success = false, Msg = "菜单简称必须唯一" });

                    var query = _commonModel.NavigationMenuRepository.Get(input.Id);
                    query.MenuName = input.MenuName;
                    query.ParentMenuId = input.ParentMenuId;
                    query.Url = input.Url;
                    query.ShortName = input.ShortName;
                    await _commonModel.NavigationMenuRepository.UpdateAsync(query);
                    return Json(new PublicOutput { Success = true, Msg = "菜单修改成功" });
                }
                catch (Exception)
                {
                    return Json(new PublicOutput { Success = false, Msg = "菜单修改失败" });
                }
            }
            //添加
            else
            {
                try
                {
                    //判断重复
                    var menuEntity = _commonModel.NavigationMenuRepository.GetAll().Where(o => o.ShortName == input.ShortName);
                    if (menuEntity.Count() > 0) return Json(new PublicOutput { Success = false, Msg = "菜单简称必须唯一" });

                    var newMenu = new NavigationMenu
                    {
                        MenuName = input.MenuName,
                        ParentMenuId = input.ParentMenuId,
                        Url = input.Url,
                    };
                    await _commonModel.NavigationMenuRepository.InsertAsync(newMenu);
                    return Json(new PublicOutput { Success = true, Msg = "菜单添加成功" });
                }
                catch (Exception)
                {
                    return Json(new PublicOutput { Success = false, Msg = "菜单添加失败" });
                }
            }
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Description("删除菜单")]
        public async Task<JsonResult> DeleteMenu(int Id)
        {
            try
            {
                var query = _commonModel.NavigationMenuRepository.Get(Id);
                //若存在子集菜单一并删除
                if (query.ChildMenuList.Count() > 0)
                {
                    //批量删除子集
                    await _commonModel.NavigationMenuRepository.DeleteAsync(o => o.ParentMenuId == Id);
                }
                await _commonModel.NavigationMenuRepository.DeleteAsync(query);
                return Json(new PublicOutput { Success = true, Msg = "菜单删除成功" });

            }
            catch (Exception)
            {
                return Json(new PublicOutput { Success = false, Msg = "菜单删除失败" });
            }
        }
    }
}