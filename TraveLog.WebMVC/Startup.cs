using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TraveLog.WebMVC.Startup))]
namespace TraveLog.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
