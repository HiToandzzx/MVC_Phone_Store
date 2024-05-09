using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HSMobile.Startup))]
namespace HSMobile
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
