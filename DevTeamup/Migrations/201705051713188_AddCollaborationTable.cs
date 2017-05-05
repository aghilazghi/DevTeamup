namespace DevTeamup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCollaborationTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Collaborations",
                c => new
                    {
                        TeamupId = c.Int(nullable: false),
                        ContributorId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.TeamupId, t.ContributorId })
                .ForeignKey("dbo.AspNetUsers", t => t.ContributorId, cascadeDelete: true)
                .ForeignKey("dbo.Teamups", t => t.TeamupId)
                .Index(t => t.TeamupId)
                .Index(t => t.ContributorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Collaborations", "TeamupId", "dbo.Teamups");
            DropForeignKey("dbo.Collaborations", "ContributorId", "dbo.AspNetUsers");
            DropIndex("dbo.Collaborations", new[] { "ContributorId" });
            DropIndex("dbo.Collaborations", new[] { "TeamupId" });
            DropTable("dbo.Collaborations");
        }
    }
}
