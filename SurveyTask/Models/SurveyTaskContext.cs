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
        }

        public static SurveyTaskContext Create()
        {
            return new SurveyTaskContext();
        }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
    }
}