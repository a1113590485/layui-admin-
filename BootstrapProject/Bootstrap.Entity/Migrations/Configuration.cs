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
        /// ����seed���������ʼ������
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(Bootstrap.Entity.Repository.EntityDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (context.UserSet.FirstOrDefault() == null)
            {
                //Ĭ���û�
                var defaultUser = new User { Sex = User.Gender.��, NickName = "������", UserName = "admin", PassWord = "46f94c8de14fb36680850768ff1b7f2a" };
                context.UserSet.Add(defaultUser);
            }

            if (context.NavigationMenuSet.FirstOrDefault() == null)
            {
                //Ĭ�ϲ˵�
                var defaultMenuList = new List<NavigationMenu>()
                {
                    new NavigationMenu { MenuName="����̨",ShortName="system_console",Url="/Manage/Home/Index"},
                    new NavigationMenu { MenuName="ϵͳ����",ShortName="system_set" },
                    new NavigationMenu { MenuName="�˵�����",ShortName="system_set_menu",Url="/Manage/Menu/Index",ParentMenuId=2 },
                    new NavigationMenu { MenuName="��ɫ����",ShortName="system_set_role",Url="/Manage/Role/Index",ParentMenuId=2},
                    new NavigationMenu { MenuName="�û�����",ShortName="system_set_user",Url="/Manage/User/Index",ParentMenuId=2 }
                };
                defaultMenuList.ForEach(s => context.NavigationMenuSet.Add(s));
            }
            
            if (context.RoleSet.FirstOrDefault() == null)
            {
                //Ĭ�Ͻ�ɫ
                var defaultRoleList = new List<Role>()
                {
                    new Role { RoleName ="����Ա",RoleDesc="����Ա��ɫ"},
                    new Role { RoleName ="�û�",RoleDesc="�û���ɫ"},
                };
                defaultRoleList.ForEach(s => context.RoleSet.Add(s));
            }

            if (context.UserRoleRelationSet.FirstOrDefault() == null)
            {
                //Ĭ���û�-��ɫ
                var defaultUserRole = new UserRoleRelation
                {
                    RoleId = 1,
                    UserId = 1,
                };
                context.UserRoleRelationSet.Add(defaultUserRole);
            }

            if (context.RolePermissionRelationSet.FirstOrDefault() == null)
            {
                //Ĭ�Ͻ�ɫ-Ȩ��
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
                //Ĭ���û�-Ȩ��
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