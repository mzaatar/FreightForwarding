using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FFSolution.Startup))]
namespace FFSolution
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
