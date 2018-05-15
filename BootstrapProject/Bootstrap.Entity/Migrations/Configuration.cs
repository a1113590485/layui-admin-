namespace Bootstrap.Entity.Migrations
{
    using Models;
    using Models.System;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Bootstrap.Entity.Repository.EntityDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        /// <summary>
        /// 重载seed方法放入初始化数据
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(Bootstrap.Entity.Repository.EntityDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (context.UserSet.FirstOrDefault() == null)
            {
                //默认用户
                var defaultUser = new User { Sex = User.Gender.男, NickName = "赵日天", UserName = "admin", PassWord = "46f94c8de14fb36680850768ff1b7f2a" };
                context.UserSet.Add(defaultUser);
            }

            if (context.NavigationMenuSet.FirstOrDefault() == null)
            {
                //默认菜单
                var defaultMenuList = new List<NavigationMenu>()
                {
                    new NavigationMenu { MenuName = "控制台",Url="/Manage/Home/Index",ShortName="manage_console" },
                    new NavigationMenu { MenuName = "系统设置" ,ShortName="system_set"},
                    new NavigationMenu { MenuName = "菜单管理",Url="/Manage/Menu/Index",ParentMenuId=2,ShortName="menu_manage" },
                    new NavigationMenu {  MenuName = "角色管理",Url="/Manage/Role/Index",ParentMenuId=2,ShortName="role_manage" }
                };
                defaultMenuList.ForEach(s => context.NavigationMenuSet.Add(s));
            }
            
            context.SaveChanges();
        }
    }
}
