namespace DevTeamup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixOriginalAddressPropertyTypo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "OriginalAddress", c => c.String());
            DropColumn("dbo.Notifications", "OriginalAdsress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "OriginalAdsress", c => c.String());
            DropColumn("dbo.Notifications", "OriginalAddress");
        }
    }
}
