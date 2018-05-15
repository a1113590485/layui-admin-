namespace Bootstrap.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reloadtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NavigationMenus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShortName = c.String(),
                        MenuName = c.String(),
                        Url = c.String(),
                        ParentMenuId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NavigationMenus", t => t.ParentMenuId)
                .Index(t => t.ParentMenuId);
            
            AddColumn("dbo.Permissions", "ShortName", c => c.String());
            CreateIndex("dbo.Permissions", "ParentPermissionId");
            AddForeignKey("dbo.Permissions", "ParentPermissionId", "dbo.Permissions", "Id");
            DropColumn("dbo.Permissions", "Guid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Permissions", "Guid", c => c.String());
            DropForeignKey("dbo.Permissions", "ParentPermissionId", "dbo.Permissions");
            DropForeignKey("dbo.NavigationMenus", "ParentMenuId", "dbo.NavigationMenus");
            DropIndex("dbo.Permissions", new[] { "ParentPermissionId" });
            DropIndex("dbo.NavigationMenus", new[] { "ParentMenuId" });
            DropColumn("dbo.Permissions", "ShortName");
            DropTable("dbo.NavigationMenus");
        }
    }
}
