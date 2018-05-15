namespace Bootstrap.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NavigationMenus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuName = c.String(),
                        Icon = c.String(),
                        Sort = c.Int(nullable: false),
                        Url = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        ParentMenuId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NavigationMenus", t => t.ParentMenuId)
                .Index(t => t.ParentMenuId);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ModuleName = c.String(),
                        Guid = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RolePermissionRelations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        PermissionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        RoleDesc = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserPermissionRelations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        PermissionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoleRelations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 8),
                        PassWord = c.String(),
                        NickName = c.String(),
                        Sex = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        LastLoginTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NavigationMenus", "ParentMenuId", "dbo.NavigationMenus");
            DropIndex("dbo.NavigationMenus", new[] { "ParentMenuId" });
            DropTable("dbo.Users");
            DropTable("dbo.UserRoleRelations");
            DropTable("dbo.UserPermissionRelations");
            DropTable("dbo.Roles");
            DropTable("dbo.RolePermissionRelations");
            DropTable("dbo.Permissions");
            DropTable("dbo.NavigationMenus");
        }
    }
}
