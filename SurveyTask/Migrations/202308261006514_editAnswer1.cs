namespace SurveyTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editAnswer1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SubmittedSurvey",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        SurveyId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Survey", t => t.SurveyId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.SurveyId);
            
            AddColumn("dbo.Answer", "SubmittedSurveId", c => c.Guid(nullable: false));
            AddColumn("dbo.Answer", "SubmittedSurvey_Id", c => c.Guid());
            CreateIndex("dbo.Answer", "SubmittedSurvey_Id");
            AddForeignKey("dbo.Answer", "SubmittedSurvey_Id", "dbo.SubmittedSurvey", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubmittedSurvey", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SubmittedSurvey", "SurveyId", "dbo.Survey");
            DropForeignKey("dbo.Answer", "SubmittedSurvey_Id", "dbo.SubmittedSurvey");
            DropIndex("dbo.SubmittedSurvey", new[] { "SurveyId" });
            DropIndex("dbo.SubmittedSurvey", new[] { "UserId" });
            DropIndex("dbo.Answer", new[] { "SubmittedSurvey_Id" });
            DropColumn("dbo.Answer", "SubmittedSurvey_Id");
            DropColumn("dbo.Answer", "SubmittedSurveId");
            DropTable("dbo.SubmittedSurvey");
        }
    }
}
