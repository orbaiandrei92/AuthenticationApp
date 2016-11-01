using Topshelf;

namespace DataMarket.Infrastructure
{
    public interface IServiceWorker
    {
        bool Start(HostControl hostControl);
        bool Stop(HostControl hostControl);

    }
}