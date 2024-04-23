using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Dokana.Startup))]
namespace Dokana
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
