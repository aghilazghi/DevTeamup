namespace DevTeamup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTeamupAndLookupsTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DevelopmentLanguages",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DevelopmentTypes",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Teamups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrganizerId = c.String(nullable: false, maxLength: 128),
                        Address = c.String(nullable: false, maxLength: 255),
                        DateTime = c.DateTime(nullable: false),
                        DevelopmentTypeId = c.Byte(nullable: false),
                        DevelopmentLanguageId = c.Byte(nullable: false),
                        Description = c.String(nullable: false, maxLength: 1024),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DevelopmentLanguages", t => t.DevelopmentLanguageId, cascadeDelete: true)
                .ForeignKey("dbo.DevelopmentTypes", t => t.DevelopmentTypeId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.OrganizerId, cascadeDelete: true)
                .Index(t => t.OrganizerId)
                .Index(t => t.DevelopmentTypeId)
                .Index(t => t.DevelopmentLanguageId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teamups", "OrganizerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Teamups", "DevelopmentTypeId", "dbo.DevelopmentTypes");
            DropForeignKey("dbo.Teamups", "DevelopmentLanguageId", "dbo.DevelopmentLanguages");
            DropIndex("dbo.Teamups", new[] { "DevelopmentLanguageId" });
            DropIndex("dbo.Teamups", new[] { "DevelopmentTypeId" });
            DropIndex("dbo.Teamups", new[] { "OrganizerId" });
            DropTable("dbo.Teamups");
            DropTable("dbo.DevelopmentTypes");
            DropTable("dbo.DevelopmentLanguages");
        }
    }
}
