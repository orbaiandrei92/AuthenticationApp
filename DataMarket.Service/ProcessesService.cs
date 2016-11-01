using DataMarket.Infrastructure;
using Topshelf;

namespace DataMarket.Service
{
    public class ProcessesService : IServiceWorker
    {
        public bool Start(HostControl hostControl)
        {
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            return true;
        }
    }
}
