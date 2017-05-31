namespace DevTeamup.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixTypos : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Replies", name: "ReplyById", newName: "RepliedById");
            RenameIndex(table: "dbo.Replies", name: "IX_ReplyById", newName: "IX_RepliedById");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Replies", name: "IX_RepliedById", newName: "IX_ReplyById");
            RenameColumn(table: "dbo.Replies", name: "RepliedById", newName: "ReplyById");
        }
    }
}
