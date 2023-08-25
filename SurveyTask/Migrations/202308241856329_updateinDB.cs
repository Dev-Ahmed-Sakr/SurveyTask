namespace SurveyTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateinDB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Answer", "QuestionId", "dbo.Question");
            DropIndex("dbo.Answer", new[] { "QuestionId" });
            DropColumn("dbo.Question", "QuestionType");
            DropTable("dbo.Answer");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Answer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Question", "QuestionType", c => c.Int(nullable: false));
            CreateIndex("dbo.Answer", "QuestionId");
            AddForeignKey("dbo.Answer", "QuestionId", "dbo.Question", "Id", cascadeDelete: true);
        }
    }
}
