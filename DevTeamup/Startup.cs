using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DevTeamup.Startup))]
namespace DevTeamup
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
