namespace DevTeamup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDiscussionAndReplyTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Discussions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostedById = c.String(nullable: false, maxLength: 128),
                        Body = c.String(nullable: false, maxLength: 250),
                        TeamupId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.PostedById, cascadeDelete: true)
                .ForeignKey("dbo.Teamups", t => t.TeamupId)
                .Index(t => t.PostedById)
                .Index(t => t.TeamupId);
            
            CreateTable(
                "dbo.Replies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RepliedById = c.String(nullable: false, maxLength: 128),
                        Body = c.String(nullable: false, maxLength: 250),
                        DiscussionId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Discussions", t => t.DiscussionId)
                .ForeignKey("dbo.AspNetUsers", t => t.RepliedById, cascadeDelete: true)
                .Index(t => t.RepliedById)
                .Index(t => t.DiscussionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Discussions", "TeamupId", "dbo.Teamups");
            DropForeignKey("dbo.Replies", "RepliedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Replies", "DiscussionId", "dbo.Discussions");
            DropForeignKey("dbo.Discussions", "PostedById", "dbo.AspNetUsers");
            DropIndex("dbo.Replies", new[] { "DiscussionId" });
            DropIndex("dbo.Replies", new[] { "RepliedById" });
            DropIndex("dbo.Discussions", new[] { "TeamupId" });
            DropIndex("dbo.Discussions", new[] { "PostedById" });
            DropTable("dbo.Replies");
            DropTable("dbo.Discussions");
        }
    }
}
