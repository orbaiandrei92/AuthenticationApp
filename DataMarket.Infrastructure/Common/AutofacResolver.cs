using Autofac;
using Autofac.Core;

namespace DataMarket.Infrastructure
{
    public class AutofacResolver : IResolver
    {
        private IComponentContext Container { get; set; }
        public AutofacResolver(IComponentContext container)
        {
            this.Container = container;
        }

        public T Resolve<T>()
        {
            T value = default(T);

            //Check to see if registered. IsRegistered will return true regardless of the lifetime scope. Odd!
            if (this.Container.IsRegistered<T>())
            {
                try
                {
                    value = this.Container.Resolve<T>();
                }
                catch (DependencyResolutionException ex)
                {
                    string errMsg = ex.Message;
                }
            }
            return value;
        }

        public T TryResolve<T>()
        {
            T instance = default(T);

            this.Container.TryResolve<T>(out instance);

            return instance;
        }
        public T ResolveNamed<T>(string serviceName)
        {
            T value = default(T);

            if (!this.Container.IsRegisteredWithName<T>(serviceName)) return value;

            try
            {
                value = this.Container.ResolveNamed<T>(serviceName);
            }
            catch (DependencyResolutionException)
            {
                //value = this.RequestResolver.Resolve<T>();
            }

            return value;
        }

        public T TryResolveNamed<T>(string serviceName)
        {

            T value = default(T);
            try
            {
                object instanceResolved;
                this.Container.TryResolveNamed(serviceName, typeof(T), out instanceResolved);
                value = (T)instanceResolved;
            }
            catch (DependencyResolutionException)
            {
                //value = this.RequestResolver.Resolve<T>();
            }

            return value;
        }
    }
}
