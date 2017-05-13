namespace DevTeamup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreatedOnAndModifiedOnPropertiesToTeamupNotificationAndUserNotification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserNotifications", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserNotifications", "ModifiedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Notifications", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Notifications", "ModifiedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Teamups", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Teamups", "ModifiedOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teamups", "ModifiedOn");
            DropColumn("dbo.Teamups", "CreatedOn");
            DropColumn("dbo.Notifications", "ModifiedOn");
            DropColumn("dbo.Notifications", "CreatedOn");
            DropColumn("dbo.UserNotifications", "ModifiedOn");
            DropColumn("dbo.UserNotifications", "CreatedOn");
        }
    }
}
