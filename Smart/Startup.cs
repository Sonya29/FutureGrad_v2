using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Smart.Startup))]
namespace Smart
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
