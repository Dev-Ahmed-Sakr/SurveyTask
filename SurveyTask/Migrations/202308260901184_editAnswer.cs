namespace SurveyTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editAnswer : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Question", "AnswerText");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Question", "AnswerText", c => c.String());
        }
    }
}
