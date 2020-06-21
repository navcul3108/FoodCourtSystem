using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FoodCourtSystem.Startup))]
namespace FoodCourtSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
