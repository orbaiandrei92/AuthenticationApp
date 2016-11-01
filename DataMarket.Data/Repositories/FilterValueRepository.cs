using DataMarket.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataMarket.Data
{
    public class FilterValueRepository : IFilterValueRepository, IDisposable
    {
        private readonly DataMarketConfigurationDbContext _dataMarketConfigurationDbContext;
        private readonly ILogger _logger;

        public FilterValueRepository(ILogger logger)
        {
            this._dataMarketConfigurationDbContext = new DataMarketConfigurationDbContext();
            this._logger = logger;
        }

        public async Task<IEnumerable<FilterValue>> GetFilterValuesByFilterId(int filterId)
        {
            var filterValuessByFilterId = _dataMarketConfigurationDbContext.FilterValues.Where(f => f.FilterId == filterId).ToList();
            _logger.Log(LoggingLevel.Info, "Number of filter values retrieved from the database : {0}", filterValuessByFilterId.Count);
            return filterValuessByFilterId;
        }

        public async Task<FilterValue> GetFilterValuesByFilterValueId(int filterValueId)
        {
            var filterValue = _dataMarketConfigurationDbContext.FilterValues.FirstOrDefault(f => f.FilterValueId == filterValueId);
            _logger.Log(LoggingLevel.Info, "Filter values retrieved from the database : {0}", filterValue.DisplayName);
            return filterValue;
        }

        public async Task<IEnumerable<FilterValue>> GetByDatamartId(int dataMartId)
        {
            var filterValuesByDatamartId = _dataMarketConfigurationDbContext.FilterValues.Where(f => f.Filters.Group.DatamartId == dataMartId).ToList();
            _logger.Log(LoggingLevel.Info, "Number of filters retrieved from the database : {0}", filterValuesByDatamartId.Count);
            return filterValuesByDatamartId;
        }

        public async Task<IEnumerable<FilterValue>> GetFilterValuesByFilterValueIds(List<int> filterValueIds)
        {
            //var f = from filterValues in _dataMarketConfigurationDbContext.FilterValues 
            //        join filters in _dataMarketConfigurationDbContext.Filters
            //        select new Combined
            //        {
            //            FilterId = filters.FilterId,

            //        }


            var filters = _dataMarketConfigurationDbContext.FilterValues.Where(f => filterValueIds.Contains(f.FilterValueId)).ToList();
            _logger.Log(LoggingLevel.Info, "Number of filter values retrieved from the database : {0}", filters.Count);
            return filters;
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
