namespace Bootstrap.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_menutable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NavigationMenus", "ParentMenuId", "dbo.NavigationMenus");
            DropIndex("dbo.NavigationMenus", new[] { "ParentMenuId" });
            AddColumn("dbo.Permissions", "ParentPermissionId", c => c.Int());
            DropTable("dbo.NavigationMenus");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Permissions", "ParentPermissionId");
            CreateIndex("dbo.NavigationMenus", "ParentMenuId");
            AddForeignKey("dbo.NavigationMenus", "ParentMenuId", "dbo.NavigationMenus", "Id");
        }
    }
}
