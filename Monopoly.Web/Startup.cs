using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Monopoly.Web.Startup))]
namespace Monopoly.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
