namespace DevTeamup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateFavoriteTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        TeamupId = c.Int(nullable: false),
                        FavoringUserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.TeamupId, t.FavoringUserId })
                .ForeignKey("dbo.AspNetUsers", t => t.FavoringUserId, cascadeDelete: true)
                .ForeignKey("dbo.Teamups", t => t.TeamupId)
                .Index(t => t.TeamupId)
                .Index(t => t.FavoringUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Favorites", "TeamupId", "dbo.Teamups");
            DropForeignKey("dbo.Favorites", "FavoringUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Favorites", new[] { "FavoringUserId" });
            DropIndex("dbo.Favorites", new[] { "TeamupId" });
            DropTable("dbo.Favorites");
        }
    }
}
