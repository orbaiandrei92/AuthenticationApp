using Autofac;
using DataMarket.Infrastructure;
using System.Diagnostics;
using Topshelf;
using Topshelf.ServiceConfigurators;
using Autofac.Configuration;

namespace DataMarket.Service
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

                x.SetDescription(string.Format("DataMarket.Service - {0}", Process.GetCurrentProcess().Id));
                x.SetDisplayName(string.Format("DataMarket.Service - {0}", Process.GetCurrentProcess().Id));
                x.SetServiceName(string.Format("DataMarket.Service - {0}", Process.GetCurrentProcess().Id));

                x.EnableShutdown();

                x.StartAutomaticallyDelayed();
                x.RunAsLocalSystem();
            });
        }
    }
}
