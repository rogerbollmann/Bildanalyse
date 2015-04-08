using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAppBildanalyse.Startup))]
namespace WebAppBildanalyse
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
