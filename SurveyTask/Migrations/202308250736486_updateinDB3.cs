namespace SurveyTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateinDB3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Question", "IsTrueFalse");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Question", "IsTrueFalse", c => c.Boolean(nullable: false));
        }
    }
}
