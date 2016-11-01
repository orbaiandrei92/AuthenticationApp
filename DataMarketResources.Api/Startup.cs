using Autofac;
using Autofac.Configuration;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(DataMarketResources.Api.Startup))]
namespace DataMarketResources.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule(new ConfigurationSettingsReader("autofac"));
            IContainer container = builder.Build();

            HttpConfiguration config = GlobalConfiguration.Configuration; 

            // DependencyResolver = new AutofacWebApiDependencyResolver(container.Resolve<ILifetimeScope>())
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container.Resolve<ILifetimeScope>());

           

            //HttpConfiguration config = new HttpConfiguration();

            appBuilder.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
              
            });

            WebApiConfig.Register(config);
            appBuilder.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            appBuilder.UseWebApi(config);         
        }
    }
}
