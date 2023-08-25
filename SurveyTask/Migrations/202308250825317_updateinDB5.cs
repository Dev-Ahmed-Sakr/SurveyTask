namespace SurveyTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateinDB5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answer", "AnswerText", c => c.String());
            AddColumn("dbo.Question", "AnswerText", c => c.String());
            DropColumn("dbo.Answer", "AnswerValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Answer", "AnswerValue", c => c.Boolean(nullable: false));
            DropColumn("dbo.Question", "AnswerText");
            DropColumn("dbo.Answer", "AnswerText");
        }
    }
}
