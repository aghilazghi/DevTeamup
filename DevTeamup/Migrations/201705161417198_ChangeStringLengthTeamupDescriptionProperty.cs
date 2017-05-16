namespace DevTeamup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeStringLengthTeamupDescriptionProperty : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Teamups", "Description", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Teamups", "Description", c => c.String(nullable: false, maxLength: 1024));
        }
    }
}
