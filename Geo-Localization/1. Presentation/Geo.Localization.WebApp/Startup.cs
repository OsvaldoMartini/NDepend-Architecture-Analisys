using Geo.Localization.WebApp;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup("Geo.Localization", typeof(Startup))]
namespace Geo.Localization.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
