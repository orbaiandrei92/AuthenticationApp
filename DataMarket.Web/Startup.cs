using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DataMarket.Web.Startup))]
namespace DataMarket.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddCors();
        //}
    }
}
