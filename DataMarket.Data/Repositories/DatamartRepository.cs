using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataMarket.Infrastructure;

namespace DataMarket.Data
{
    public class DatamartRepository : IDatamartRepository
    {
        private readonly DataMarketConfigurationDbContext _dataMarketConfigurationDbContext;
        private readonly ILogger _logger;

        public DatamartRepository(ILogger logger)
        {
            this._dataMarketConfigurationDbContext = new DataMarketConfigurationDbContext();
            this._logger = logger;
        }

        public async Task<IEnumerable<Datamart>> GetAll()
        {
            var datamarts = _dataMarketConfigurationDbContext.Datamarts.ToList();
            _logger.Log(LoggingLevel.Info, "Number of datamarts retrieved from the database : {0}", datamarts.Count);
            return datamarts;
        }

        public async Task<Datamart> GetById(int id)
        {
            var datamart = _dataMarketConfigurationDbContext.Datamarts.Where(d => d.DatamartId == id).FirstOrDefault();
            _logger.Log(LoggingLevel.Info, "One datamart retrieved from the database");
            return datamart;
        }

        public async Task<Datamart> GetDatamartsByDatamartsId(int datamartId)
        {
            var datamart = _dataMarketConfigurationDbContext.Datamarts.FirstOrDefault(d => d.DatamartId == datamartId);
            if (datamart == null)
            {
                _logger.Log(LoggingLevel.Info, "Datamart with id {0} not found", datamartId);
            }

            _logger.Log(LoggingLevel.Info, "Number of datamarts retrieved from the database : {0}", datamart);
            return datamart;
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