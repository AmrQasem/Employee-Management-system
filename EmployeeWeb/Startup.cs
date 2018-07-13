using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EmployeeWeb.Startup))]
namespace EmployeeWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
