using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebMVCHelp.Startup))]
namespace WebMVCHelp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
