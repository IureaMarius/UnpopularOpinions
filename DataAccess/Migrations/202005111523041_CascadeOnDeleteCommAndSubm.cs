namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CascadeOnDeleteCommAndSubm : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "ParentComment_Id", "dbo.Comments");
            DropForeignKey("dbo.Comments", "ParentSubmission_Id", "dbo.Submissions");
            RenameColumn(table: "dbo.Submissions", name: "ParentSubmission_Id", newName: "Comments_Id");
            AddColumn("dbo.Comments", "Replies_Id", c => c.Guid());
            CreateIndex("dbo.Comments", "Replies_Id");
            CreateIndex("dbo.Submissions", "Comments_Id");
            AddForeignKey("dbo.Comments", "Replies_Id", "dbo.Comments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Submissions", "Comments_Id", "dbo.Comments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Submissions", "Comments_Id", "dbo.Comments");
            DropForeignKey("dbo.Comments", "Replies_Id", "dbo.Comments");
            DropIndex("dbo.Submissions", new[] { "Comments_Id" });
            DropIndex("dbo.Comments", new[] { "Replies_Id" });
            DropColumn("dbo.Comments", "Replies_Id");
            RenameColumn(table: "dbo.Submissions", name: "Comments_Id", newName: "ParentSubmission_Id");
            AddForeignKey("dbo.Comments", "ParentSubmission_Id", "dbo.Submissions", "Id");
            AddForeignKey("dbo.Comments", "ParentComment_Id", "dbo.Comments", "Id");
        }
    }
}
