using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SurveyTask.Startup))]
namespace SurveyTask
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
