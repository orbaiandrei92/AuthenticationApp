using System;
using Microsoft.Owin.Hosting;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;
using Owin;
using Newtonsoft.Json.Converters;
using Autofac.Integration.WebApi;
using Autofac;
using Topshelf;
using DataMarket.Infrastructure;
using System.Net.Http;

namespace DataMarket.Api
{
    public class DataMarketWorkerService: IServiceWorker
    {
        private const string Bindings = "Bindings";
        private IResolver Resolver { get; set; }
        private ILogger Logger { get; set; }
        private ILifetimeScope LifeTimeScope { get; set; }
        private IDisposable WebApplication { get; set; }
        private List<string> Urls { get; set; }

        public DataMarketWorkerService(ILogger logger, IResolver resolver, ILifetimeScope lifeTimeScope)
        {
            this.Logger = logger;
            this.Resolver = resolver;
            this.LifeTimeScope = lifeTimeScope;
            this.ProcessUrls();
        }

        private void ProcessUrls()
        {
            this.Urls = new List<string>(ConfigurationManager.AppSettings[Bindings].Split(new char[] { ';' }));
        }
        public bool Start(HostControl hostControl)
        {
            bool started = true;

            this.Logger.Log(LoggingLevel.Debug, "Creating Api Application Service");

            try
            {
                StartOptions options = new StartOptions();

                this.Urls.ForEach(url =>
                {
                    options.Urls.Add(url.Trim());
                });

                this.WebApplication = WebApp.Start(options, (appBuilder) =>
                {
                    HttpConfiguration configuration = new HttpConfiguration
                    {
                        DependencyResolver = new AutofacWebApiDependencyResolver(this.LifeTimeScope)
                    };
                    configuration.Formatters.Clear();
                    configuration.Formatters.Add(new JsonMediaTypeFormatter());
                    configuration.Formatters.JsonFormatter.SerializerSettings =
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    };
                    configuration.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());
                    configuration.MapHttpAttributeRoutes();
                    configuration.EnableCors();
                    appBuilder.UseWebApi(configuration);
                    appBuilder.UseWelcomePage("/");
                    
                });
                this.Logger.Log(LoggingLevel.Debug, "Api Application Service Started");

            }
            catch (Exception ex)
            {
                started = false;
                throw new Exception("Error creating Api Application Service", ex);
            }
            return started;
        }

        public bool Stop(HostControl hostControl)
        {
            bool stopped = true;
            this.Logger.Log(LoggingLevel.Debug, "Stopping Api Application Service");

            try
            {
                this.WebApplication.Dispose();
                this.Logger.Log(LoggingLevel.Debug, "Stopped Api Application Service");
            }
            catch (Exception ex)
            {
                stopped = false;
                this.Logger.Log(LoggingLevel.Debug, "Error stopping Api Application Service", ex);
            }

            return stopped;
        }
    }
}