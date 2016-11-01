using DataMarket.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMarket.Data
{
    public class TestRepository : ITestRepository, IDisposable
    {

        private readonly DataMarketDbContext _dataMarketDbContext;
        private readonly ILogger _logger;

        public TestRepository(ILogger logger)
        {
            this._dataMarketDbContext = new DataMarketDbContext();
            this._logger = logger;
        }

        public IEnumerable<TestEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Filter> GetFiltersByGroupId(int groupId)
        {
            var filters = new List<Filter>();

            //var group = this._dataMarketDbContext.Groups.Find(groupId);

            //if(group == null)
            //{
            //    this._logger.Log(LoggingLevel.Error, "No group found with id : {0}", groupId);
            //    return null;
            //}

            //var filters = group.Filters;

            _logger.Log(LoggingLevel.Info, "Number of filters retrieved from the database : {0}", filters.Count);
            return filters;
        }

        public async Task<IEnumerable<Filter>> GetFiltersByGroupIdAsync(int groupId)
        {
            var filters = new List<Filter>();
            var filter1 = new Filter();
            filter1.FilterId = 1;
            filter1.FilterName = "Filter1";
            filter1.GroupId = 2;

            var filter2 = new Filter();
            filter2.FilterId = 2;
            filter2.FilterName = "Filter2";
            filter2.GroupId = 4;

            filters.Add(filter1);
            filters.Add(filter2);

            //var group = await _dataMarketDbContext.Groups.FindAsync(groupId);

            //if (group == null)
            //{
            //    var errorMessage = string.Format("No group found with id : {0}", groupId);
            //    this._logger.Log(LoggingLevel.Error, errorMessage);
            //    throw new Exception(errorMessage);
            //}

            //var filters = group.Filters;

            _logger.Log(LoggingLevel.Info, "Number of filters retrieved from the database : {0}", filters.Count);
            return filters;
        }

        public void Dispose()
        {
            if(_dataMarketDbContext !=null)
            {
                _dataMarketDbContext.Dispose();
            }
        }

    }
}
