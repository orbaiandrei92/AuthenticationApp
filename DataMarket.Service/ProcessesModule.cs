using Autofac;
using System.Reflection;

namespace DataMarket.Service
{
    public class ProcessesModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
        }
    }
}
