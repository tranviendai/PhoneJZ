using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PhoneJZ.Startup))]
namespace PhoneJZ
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
