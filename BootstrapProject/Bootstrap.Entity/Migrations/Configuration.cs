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
                    new NavigationMenu { MenuName="控制台",ShortName="system_console",Url="/Manage/Home/Index"},
                    new NavigationMenu { MenuName="系统设置",ShortName="system_set" },
                    new NavigationMenu { MenuName="菜单管理",ShortName="system_set_menu",Url="/Manage/Menu/Index",ParentMenuId=2 },
                    new NavigationMenu { MenuName="角色管理",ShortName="system_set_role",Url="/Manage/Role/Index",ParentMenuId=2},
                    new NavigationMenu { MenuName="用户管理",ShortName="system_set_user",Url="/Manage/User/Index",ParentMenuId=2 }
                };
                defaultMenuList.ForEach(s => context.NavigationMenuSet.Add(s));
            }
            
            if (context.RoleSet.FirstOrDefault() == null)
            {
                //默认角色
                var defaultRoleList = new List<Role>()
                {
                    new Role { RoleName ="管理员",RoleDesc="管理员角色"},
                    new Role { RoleName ="用户",RoleDesc="用户角色"},
                };
                defaultRoleList.ForEach(s => context.RoleSet.Add(s));
            }

            if (context.UserRoleRelationSet.FirstOrDefault() == null)
            {
                //默认用户-角色
                var defaultUserRole = new UserRoleRelation
                {
                    RoleId = 1,
                    UserId = 1,
                };
                context.UserRoleRelationSet.Add(defaultUserRole);
            }

            if (context.RolePermissionRelationSet.FirstOrDefault() == null)
            {
                //默认角色-权限
                var defaultRolePermissionList = new List<RolePermissionRelation>
                {
                    new RolePermissionRelation { RoleId = 1,PermissionName="/Manage/Home"},
                    new RolePermissionRelation { RoleId = 1,PermissionName="/Manage/Home/Index"},
                    new RolePermissionRelation { RoleId = 1,PermissionName="/Manage/Menu"},
                    new RolePermissionRelation { RoleId = 1,PermissionName="/Manage/Menu/Index"},
                    new RolePermissionRelation { RoleId = 1,PermissionName="/Manage/Menu/AddOrEidtMenuView"},
                    new RolePermissionRelation { RoleId = 1,PermissionName="/Manage/Menu/GetMenuTree"},
                    new RolePermissionRelation { RoleId = 1,PermissionName="/Manage/Menu/AddOrEidtMenu"},
                    new RolePermissionRelation { RoleId = 1,PermissionName="/Manage/Menu/DeleteMenu"},
                    new RolePermissionRelation { RoleId = 1,PermissionName="/Manage/Role"},
                    new RolePermissionRelation { RoleId = 1,PermissionName="/Manage/Role/Index"},
                    new RolePermissionRelation { RoleId = 1,PermissionName="/Manage/Role/AddOrEidtRoleView"},
                    new RolePermissionRelation { RoleId = 1,PermissionName="/Manage/Role/ChangePermissionView"},
                    new RolePermissionRelation { RoleId = 1,PermissionName="/Manage/Role/AddOrEditRole"},
                    new RolePermissionRelation { RoleId = 1,PermissionName="/Manage/Role/BatchDeleteRole"},
                    new RolePermissionRelation { RoleId = 1,PermissionName="/Manage/Role/GetRoleList"},
                    new RolePermissionRelation { RoleId = 1,PermissionName="/Manage/Role/DeleteRole"},
                    new RolePermissionRelation { RoleId = 1,PermissionName="/Manage/Role/GetRolePermissionList"},
                    new RolePermissionRelation { RoleId = 1,PermissionName="/Manage/Role/SaveRolePermission"},
                    new RolePermissionRelation { RoleId = 1,PermissionName="/Manage/User"},
                    new RolePermissionRelation { RoleId = 1,PermissionName="/Manage/User/Index"},
                    new RolePermissionRelation { RoleId = 1,PermissionName="/Manage/User/ChangePermissionView"},
                    new RolePermissionRelation { RoleId = 1,PermissionName="/Manage/User/AddOrEditUserView"},
                    new RolePermissionRelation { RoleId = 1,PermissionName="/Manage/User/GetUserList"},
                    new RolePermissionRelation { RoleId = 1,PermissionName="/Manage/User/SaveUserPermission"},
                    new RolePermissionRelation { RoleId = 1,PermissionName="/Manage/User/SaveUserInfo"},
                };
                defaultRolePermissionList.ForEach(s=>context.RolePermissionRelationSet.Add(s));
            }

            if (context.UserPermissionRelationSet.FirstOrDefault() == null)
            {
                //默认用户-权限
                var defaultUserPermissionList = new List<UserPermissionRelation>
                {
                    new UserPermissionRelation { UserId = 1,PermissionName="/Manage/Home"},
                    new UserPermissionRelation { UserId = 1,PermissionName="/Manage/Home/Index"},
                    new UserPermissionRelation { UserId = 1,PermissionName="/Manage/Menu"},
                    new UserPermissionRelation { UserId = 1,PermissionName="/Manage/Menu/Index"},
                    new UserPermissionRelation { UserId = 1,PermissionName="/Manage/Menu/AddOrEidtMenuView"},
                    new UserPermissionRelation { UserId = 1,PermissionName="/Manage/Menu/GetMenuTree"},
                    new UserPermissionRelation { UserId = 1,PermissionName="/Manage/Menu/AddOrEidtMenu"},
                    new UserPermissionRelation { UserId = 1,PermissionName="/Manage/Menu/DeleteMenu"},
                    new UserPermissionRelation { UserId = 1,PermissionName="/Manage/Role"},
                    new UserPermissionRelation { UserId = 1,PermissionName="/Manage/Role/Index"},
                    new UserPermissionRelation { UserId = 1,PermissionName="/Manage/Role/AddOrEidtRoleView"},
                    new UserPermissionRelation { UserId = 1,PermissionName="/Manage/Role/ChangePermissionView"},
                    new UserPermissionRelation { UserId = 1,PermissionName="/Manage/Role/AddOrEditRole"},
                    new UserPermissionRelation { UserId = 1,PermissionName="/Manage/Role/BatchDeleteRole"},
                    new UserPermissionRelation { UserId = 1,PermissionName="/Manage/Role/GetRoleList"},
                    new UserPermissionRelation { UserId = 1,PermissionName="/Manage/Role/DeleteRole"},
                    new UserPermissionRelation { UserId = 1,PermissionName="/Manage/Role/GetRolePermissionList"},
                    new UserPermissionRelation { UserId = 1,PermissionName="/Manage/Role/SaveRolePermission"},
                    new UserPermissionRelation { UserId = 1,PermissionName="/Manage/User"},
                    new UserPermissionRelation { UserId = 1,PermissionName="/Manage/User/Index"},
                    new UserPermissionRelation { UserId = 1,PermissionName="/Manage/User/ChangePermissionView"},
                    new UserPermissionRelation { UserId = 1,PermissionName="/Manage/User/AddOrEditUserView"},
                    new UserPermissionRelation { UserId = 1,PermissionName="/Manage/User/GetUserList"},
                    new UserPermissionRelation { UserId = 1,PermissionName="/Manage/User/SaveUserPermission"},
                    new UserPermissionRelation { UserId = 1,PermissionName="/Manage/User/SaveUserInfo"},
                };
                defaultUserPermissionList.ForEach(s => context.UserPermissionRelationSet.Add(s));
            }
            context.SaveChanges();
        }
    }
}