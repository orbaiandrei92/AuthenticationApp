using System.ServiceProcess;
using System.Timers;
using DataMarket.WindowsService;

namespace DataMarket.WindowsService
{
    public partial class MyService : ServiceBase
    {
        private MyRepository _repo;
        private MyManager _myManager;
        private Timer timer;

        //  private Timer timer = null;

        public MyService()
        {
            _repo = new MyRepository();
            _myManager = new MyManager();

            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer = new Timer();
            this.timer.Interval = 60000;// Take Queued Lists each 1 minute
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_Thick);
            this.timer.Enabled = true;
          

            DoWork();
            Service.WriteErrorLog("The service has started");

        }

        public void DoWork()
        {
            Service.WriteErrorLog("Start processing");

            this.timer.Stop();
            while (QueuedListCount() > 0)
            {
              
                //Do some work
                TakeMyLists();
            }
            this.timer.Start();

            Service.WriteErrorLog("Complete processing ");
        }

        private void timer_Thick(object sender, ElapsedEventArgs e)
        {
            DoWork();
            Service.WriteErrorLog("Timer ticked and Queued Lists were processed sucessfully!");
        }

        public int QueuedListCount()
        {

            return _repo.GetListsByStatus(StatusLevel.Queued.ToString()).Count;
        }

        public void TakeMyLists()
        {
            var myList = _repo.GetListsByStatus(StatusLevel.Queued.ToString());
            Service.WriteErrorLog("My lists with 'Queued' status:");

            foreach (var y in myList)
            {
                Service.WriteErrorLog(string.Format("The list: {0}, with status: {1}, created on: {2}", y.ListName, y.Status.DisplayName, y.CreatedDate));
            }

            using (var sequenceEnum = myList.GetEnumerator())
            {
                while (sequenceEnum.MoveNext())
                {
                    try
                    {
                        var temporaryCountTable = string.Format("TempCountCopy_{0}", sequenceEnum.Current.SavedFilterId);
                        var SequenceTable = string.Format("SequenceForList_{0}", sequenceEnum.Current.SavedFilterId);

                        _myManager.SetNewStatus(sequenceEnum.Current.SavedFilterId, StatusLevel.Waiting.ToString());
                        // Do something with sequenceEnum.Current. 
                        var selectString = _myManager.FiltersToSelect(sequenceEnum.Current.SavedFilterId);
                        var query = _myManager.CreateTheQuery(sequenceEnum.Current.SavedFilterId, selectString, temporaryCountTable);
                        _myManager.ExecuteQuery(query, false);
                        _myManager.GetEachCount(sequenceEnum.Current.SavedFilterId, temporaryCountTable);
                        _myManager.SetNewStatus(sequenceEnum.Current.SavedFilterId, StatusLevel.Ready.ToString());
                        //Sequence part
                        var querySequence = _myManager.CreateTheQuery(sequenceEnum.Current.SavedFilterId, "Sequence", SequenceTable);
                        _myManager.ExecuteQuery(querySequence, false);
                    }
                    catch
                    {
                        _myManager.SetNewStatus(sequenceEnum.Current.SavedFilterId, StatusLevel.Failed.ToString());
                    }
                }
            }
        }

        protected override void OnStop()
        {
            Service.WriteErrorLog("The service has stopped");
        }
    }
}
