using Autofac;
using Autofac.Integration.WebApi;
using DataMarket.Business;
using DataMarket.Infrastructure;
using DataMarket.Data;
using System.Reflection;
using DataMarket.Infrastructure.Common;

namespace DataMarket.Api
{
    public class DataMarketModule: Autofac.Module
    {
        //TODO don't forget to instantiate all of your dependecies here
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(context => new AutofacResolver(context.Resolve<IComponentContext>()))
                .As<IResolver>()
                .SingleInstance();

            builder.Register(c => new NLogLogger())
               .As<ILogger>()
               .SingleInstance();

            builder.Register(c => new DataMarketWorkerService(c.Resolve<ILogger>(), c.Resolve<IResolver>(), c.Resolve<ILifetimeScope>()))
                .As<IServiceWorker>()
                .SingleInstance();

            AutomapperConfig.InitializeMappings();

            builder.Register(c => new TestRepository(c.Resolve<ILogger>()))
               .As<ITestRepository>()
               .InstancePerRequest();

            builder.Register(c => new DatamartRepository(c.Resolve<ILogger>()))
               .As<IDatamartRepository>()
               .InstancePerRequest();

            builder.Register(c => new FilterRepository(c.Resolve<ILogger>()))
              .As<IFilterRepository>()
              .InstancePerRequest();

            builder.Register(c => new AutomapperResolver())
               .As<IMapperResolver>()
               .SingleInstance();

            builder.Register(c => new GroupRepository(c.Resolve<ILogger>()))
              .As<IGroupRepository>()
              .InstancePerRequest();

            builder.Register(c => new FilterValueRepository(c.Resolve<ILogger>()))
              .As<IFilterValueRepository>()
              .InstancePerRequest();

            builder.Register(c => new FilterManager(c.Resolve<ILogger>(), c.Resolve<IMapperResolver>(), c.Resolve<IDatamartRepository>(), c.Resolve<IFilterRepository>(), c.Resolve<IGroupRepository>(), c.Resolve<IFilterValueRepository>()))
               .As<IFilterManager>()
               .InstancePerRequest();

            builder.Register(c => new DatamartManager(c.Resolve<ILogger>(), c.Resolve<IMapperResolver>(), c.Resolve<IDatamartRepository>()))
               .As<IDatamartManager>()
               .InstancePerRequest();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
        }
    }
}