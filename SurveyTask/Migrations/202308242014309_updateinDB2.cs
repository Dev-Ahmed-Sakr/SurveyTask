namespace SurveyTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateinDB2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Question", "IsTrueFalse", c => c.Boolean(nullable: false));
            AddColumn("dbo.Survey", "CreatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Survey", "CreatedDate");
            DropColumn("dbo.Question", "IsTrueFalse");
        }
    }
}
