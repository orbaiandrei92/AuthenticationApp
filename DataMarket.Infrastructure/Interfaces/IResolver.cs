namespace DataMarket.Infrastructure
{
    public interface IResolver
    {
        T Resolve<T>();
        T TryResolve<T>();
        T ResolveNamed<T>(string serviceName);
        T TryResolveNamed<T>(string serviceName);
    }
}
