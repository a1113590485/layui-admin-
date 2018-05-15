namespace Bootstrap.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_rolePermissiontable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RolePermissionRelations", "PermissionName", c => c.String());
            DropColumn("dbo.RolePermissionRelations", "PermissionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RolePermissionRelations", "PermissionId", c => c.Int(nullable: false));
            DropColumn("dbo.RolePermissionRelations", "PermissionName");
        }
    }
}
