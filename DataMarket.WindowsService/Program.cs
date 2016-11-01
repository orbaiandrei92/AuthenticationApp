using System.ServiceProcess;

namespace DataMarket.WindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new MyService() 
            };            
            ServiceBase.Run(ServicesToRun);

           //MyService myService = new MyService();
           //myService.TakeMyLists();
        }
    }
}
