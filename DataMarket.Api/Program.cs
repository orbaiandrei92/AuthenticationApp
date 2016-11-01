using Autofac;
using Autofac.Configuration;
using DataMarket.Infrastructure;
using System.Diagnostics;
using Topshelf;
using Topshelf.ServiceConfigurators;

namespace DataMarket.Api
{
    public class Program
    {
        static void Main(string[] args)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule(new ConfigurationSettingsReader("autofac"));
            IContainer container = builder.Build();

            HostFactory.Run(x =>
            {
                x.Service<IServiceWorker>((ServiceConfigurator<IServiceWorker> sc) =>
                {
                    sc.ConstructUsing(hostSettings => container.Resolve<IServiceWorker>());
                    sc.WhenStarted((service, hc) => service.Start(hc));
                    sc.WhenStopped((service, hc) => service.Stop(hc));
                });

                x.SetDescription(string.Format("DataMarket.Api - {0}", Process.GetCurrentProcess().Id));
                x.SetDisplayName(string.Format("DataMarket.Api - {0}", Process.GetCurrentProcess().Id));
                x.SetServiceName(string.Format("DataMarket.Api - {0}", Process.GetCurrentProcess().Id));

                x.EnableShutdown();

                x.StartAutomaticallyDelayed();
                x.RunAsLocalSystem();
            });
        }
    }
}