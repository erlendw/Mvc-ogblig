using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mvc_oblig.Startup))]
namespace Mvc_oblig
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
