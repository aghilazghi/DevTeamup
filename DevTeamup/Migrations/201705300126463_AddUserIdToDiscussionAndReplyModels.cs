namespace DevTeamup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIdToDiscussionAndReplyModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Discussions", "PostedByName", c => c.String(nullable: false));
            AddColumn("dbo.Discussions", "PostedById", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Replies", "RepliedByName", c => c.String(nullable: false));
            AddColumn("dbo.Replies", "ReplyById", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Discussions", "PostedById");
            CreateIndex("dbo.Replies", "ReplyById");
            AddForeignKey("dbo.Discussions", "PostedById", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Replies", "ReplyById", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.Discussions", "PostedBy");
            DropColumn("dbo.Replies", "RepliedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Replies", "RepliedBy", c => c.String(nullable: false));
            AddColumn("dbo.Discussions", "PostedBy", c => c.String(nullable: false));
            DropForeignKey("dbo.Replies", "ReplyById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Discussions", "PostedById", "dbo.AspNetUsers");
            DropIndex("dbo.Replies", new[] { "ReplyById" });
            DropIndex("dbo.Discussions", new[] { "PostedById" });
            DropColumn("dbo.Replies", "ReplyById");
            DropColumn("dbo.Replies", "RepliedByName");
            DropColumn("dbo.Discussions", "PostedById");
            DropColumn("dbo.Discussions", "PostedByName");
        }
    }
}
