namespace YatraNepal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdminInfoes", "AdminId", c => c.Guid(nullable: false));
            DropColumn("dbo.AdminInfoes", "AgentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AdminInfoes", "AgentId", c => c.Guid(nullable: false));
            DropColumn("dbo.AdminInfoes", "AdminId");
        }
    }
}
