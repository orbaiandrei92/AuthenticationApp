using DataMarket.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace DataMarket.Data
{
    public class FilterRepository : IFilterRepository, IDisposable
    {
        private readonly DataMarketConfigurationDbContext _dataMarketConfigurationDbContext;
        private readonly ILogger _logger;

        public FilterRepository(ILogger logger)
        {
            this._dataMarketConfigurationDbContext = new DataMarketConfigurationDbContext();
            this._logger = logger;
        }

        public async Task<IEnumerable<Filter>> GetAll()
        {
            var filters = _dataMarketConfigurationDbContext.Filters;
            _logger.Log(LoggingLevel.Info, "Number of filters retrieved from the database : {0}", filters.ToList().Count);
            return filters;
        }

        public async Task<Filter> GetById(int id)
        {
            var filter = _dataMarketConfigurationDbContext.Filters.Where(f => f.FilterId == id).FirstOrDefault();
            _logger.Log(LoggingLevel.Info, "One filter retrieved from the database");
            return filter;
        }

        public async Task<IEnumerable<Filter>> GetByDatamartId(int dataMartId)
        {
            var filtersByDatamartId = _dataMarketConfigurationDbContext.Filters.Where(f => f.Group.DatamartId == dataMartId).ToList();
            _logger.Log(LoggingLevel.Info, "Number of filters retrieved from the database : {0}", filtersByDatamartId.Count);
            return filtersByDatamartId;
        }

        public async Task<IEnumerable<Filter>> GetByGroupId(int groupId)
        {
            var filtersByGroupId = _dataMarketConfigurationDbContext.Filters.Where(f => f.GroupId == groupId).ToList();
            _logger.Log(LoggingLevel.Info, "Number of filters retrieved from the database : {0}", filtersByGroupId.Count);
            return filtersByGroupId;
        }

        public async Task<IEnumerable<Filter>> GetFiltersByFiltersId(List<int> filters)
        {
            var filtersName = _dataMarketConfigurationDbContext.Filters.Where(f => filters.Contains(f.FilterId)).ToList();
            _logger.Log(LoggingLevel.Info, "Number of filters retrieved from the database : {0}", filtersName.Count);
            return filtersName;

        }

        public void Dispose()
        {
            if (_dataMarketConfigurationDbContext != null)
            {
                _dataMarketConfigurationDbContext.Dispose();
            }
        }       
    }
}