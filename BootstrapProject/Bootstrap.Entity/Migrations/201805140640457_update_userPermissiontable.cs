namespace Bootstrap.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_userPermissiontable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserPermissionRelations", "PermissionName", c => c.String());
            DropColumn("dbo.UserPermissionRelations", "PermissionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserPermissionRelations", "PermissionId", c => c.Int(nullable: false));
            DropColumn("dbo.UserPermissionRelations", "PermissionName");
        }
    }
}
