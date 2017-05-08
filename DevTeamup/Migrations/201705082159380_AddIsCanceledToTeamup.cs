namespace DevTeamup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsCanceledToTeamup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teamups", "IsCanceled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teamups", "IsCanceled");
        }
    }
}
