namespace Bootstrap.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_usertable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "UserStatus");
        }
    }
}
