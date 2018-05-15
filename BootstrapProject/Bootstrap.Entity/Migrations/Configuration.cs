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
                    new NavigationMenu { MenuName = "����̨",Url="/Manage/Home/Index",ShortName="manage_console" },
                    new NavigationMenu { MenuName = "ϵͳ����" ,ShortName="system_set"},
                    new NavigationMenu { MenuName = "�˵�����",Url="/Manage/Menu/Index",ParentMenuId=2,ShortName="menu_manage" },
                    new NavigationMenu {  MenuName = "��ɫ����",Url="/Manage/Role/Index",ParentMenuId=2,ShortName="role_manage" }
                };
                defaultMenuList.ForEach(s => context.NavigationMenuSet.Add(s));
            }
            
            context.SaveChanges();
        }
    }
}
