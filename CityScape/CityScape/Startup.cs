using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CityScape.Startup))]
namespace CityScape
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
