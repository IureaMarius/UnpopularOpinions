namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Text = c.String(),
                        Upvotes = c.Int(nullable: false),
                        Downvotes = c.Int(nullable: false),
                        NrOfReplies = c.Int(nullable: false),
                        Author_Id = c.Guid(),
                        ParentSubmission_Id = c.Guid(),
                        ParentComment_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Author_Id)
                .ForeignKey("dbo.Submissions", t => t.ParentSubmission_Id)
                .ForeignKey("dbo.Comments", t => t.ParentComment_Id)
                .Index(t => t.Author_Id)
                .Index(t => t.ParentSubmission_Id)
                .Index(t => t.ParentComment_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Submissions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Content = c.String(),
                        Upvotes = c.Int(nullable: false),
                        Downvotes = c.Int(nullable: false),
                        Author_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Author_Id)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SubOrCommId = c.Guid(nullable: false),
                        VoteValue = c.Int(nullable: false),
                        VoteType = c.Int(nullable: false),
                        VoterId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "ParentComment_Id", "dbo.Comments");
            DropForeignKey("dbo.Comments", "ParentSubmission_Id", "dbo.Submissions");
            DropForeignKey("dbo.Submissions", "Author_Id", "dbo.Users");
            DropForeignKey("dbo.Comments", "Author_Id", "dbo.Users");
            DropIndex("dbo.Submissions", new[] { "Author_Id" });
            DropIndex("dbo.Comments", new[] { "ParentComment_Id" });
            DropIndex("dbo.Comments", new[] { "ParentSubmission_Id" });
            DropIndex("dbo.Comments", new[] { "Author_Id" });
            DropTable("dbo.Votes");
            DropTable("dbo.Submissions");
            DropTable("dbo.Users");
            DropTable("dbo.Comments");
        }
    }
}
