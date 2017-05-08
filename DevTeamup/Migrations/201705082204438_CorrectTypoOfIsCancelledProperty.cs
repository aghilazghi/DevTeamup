namespace DevTeamup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectTypoOfIsCancelledProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teamups", "IsCancelled", c => c.Boolean(nullable: false));
            DropColumn("dbo.Teamups", "IsCanceled");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teamups", "IsCanceled", c => c.Boolean(nullable: false));
            DropColumn("dbo.Teamups", "IsCancelled");
        }
    }
}
