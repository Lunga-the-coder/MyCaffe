using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyCaffe.Startup))]
namespace MyCaffe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
