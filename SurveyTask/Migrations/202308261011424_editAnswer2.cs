namespace SurveyTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editAnswer2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SubmittedSurvey", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.SubmittedSurvey", new[] { "UserId" });
            DropColumn("dbo.SubmittedSurvey", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SubmittedSurvey", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.SubmittedSurvey", "UserId");
            AddForeignKey("dbo.SubmittedSurvey", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
