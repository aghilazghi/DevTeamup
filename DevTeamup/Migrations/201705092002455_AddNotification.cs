namespace DevTeamup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        NotificationType = c.Int(nullable: false),
                        OriginalDateTime = c.DateTime(),
                        OriginalAdsress = c.String(),
                        Teamup_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teamups", t => t.Teamup_Id, cascadeDelete: true)
                .Index(t => t.Teamup_Id);
            
            CreateTable(
                "dbo.UserNotifications",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        NotificationId = c.Int(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.NotificationId })
                .ForeignKey("dbo.Notifications", t => t.NotificationId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.NotificationId);
            
            AddColumn("dbo.Teamups", "IsCanceled", c => c.Boolean(nullable: false));
            DropColumn("dbo.Teamups", "IsCancelled");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teamups", "IsCancelled", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.UserNotifications", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserNotifications", "NotificationId", "dbo.Notifications");
            DropForeignKey("dbo.Notifications", "Teamup_Id", "dbo.Teamups");
            DropIndex("dbo.UserNotifications", new[] { "NotificationId" });
            DropIndex("dbo.UserNotifications", new[] { "UserId" });
            DropIndex("dbo.Notifications", new[] { "Teamup_Id" });
            DropColumn("dbo.Teamups", "IsCanceled");
            DropTable("dbo.UserNotifications");
            DropTable("dbo.Notifications");
        }
    }
}
