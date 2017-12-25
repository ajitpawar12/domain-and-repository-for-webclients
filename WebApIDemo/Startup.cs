using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApIDemo.Startup))]
namespace WebApIDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
