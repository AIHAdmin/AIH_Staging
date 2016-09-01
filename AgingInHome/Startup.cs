using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AgingInHome.Startup))]
namespace AgingInHome
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
