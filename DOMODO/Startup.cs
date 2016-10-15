using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DOMODO.Startup))]
namespace DOMODO
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
