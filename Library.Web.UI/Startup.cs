using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Library.Web.UI.Startup))]
namespace Library.Web.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
