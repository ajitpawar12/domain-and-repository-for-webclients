using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebSiteClient.Startup))]
namespace WebSiteClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
