namespace DevTeamup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDiscusionPostedByIdToPostedBy : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Discussions", "PostedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Replies", "RepliedById", "dbo.AspNetUsers");
            DropIndex("dbo.Discussions", new[] { "PostedById" });
            DropIndex("dbo.Replies", new[] { "RepliedById" });
            AddColumn("dbo.Discussions", "PostedBy", c => c.String(nullable: false));
            AddColumn("dbo.Replies", "RepliedBy", c => c.String(nullable: false));
            DropColumn("dbo.Discussions", "PostedById");
            DropColumn("dbo.Replies", "RepliedById");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Replies", "RepliedById", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Discussions", "PostedById", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Replies", "RepliedBy");
            DropColumn("dbo.Discussions", "PostedBy");
            CreateIndex("dbo.Replies", "RepliedById");
            CreateIndex("dbo.Discussions", "PostedById");
            AddForeignKey("dbo.Replies", "RepliedById", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Discussions", "PostedById", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
