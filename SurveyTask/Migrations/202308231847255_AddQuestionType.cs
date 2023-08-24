namespace SurveyTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuestionType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Question", "QuestionType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Question", "QuestionType");
        }
    }
}
