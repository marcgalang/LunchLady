using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LunchLady.Startup))]
namespace LunchLady
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
