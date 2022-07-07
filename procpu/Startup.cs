using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(procpu.Startup))]
namespace procpu
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
