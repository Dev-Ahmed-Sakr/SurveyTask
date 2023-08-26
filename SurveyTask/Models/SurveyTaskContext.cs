using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SurveyTask.Models
{
    public class SurveyTaskContext : IdentityDbContext<ApplicationUser>
    {
        public SurveyTaskContext() : base("DefaultConnection", throwIfV1Schema: false) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Question>()
                .HasRequired(q => q.Survey)
                .WithMany(s => s.Questions)
                .HasForeignKey(q => q.SurveyId);
        }

        public static SurveyTaskContext Create()
        {
            return new SurveyTaskContext();
        }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SubmittedSurvey> SubmittedSurvey { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

    }
}