using DataMarket.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Data.SqlClient;

namespace DataMarket.Data
{
    public class SavedFilterRepository : ISavedFilterRepository
    {
        private readonly IListInProgressRepository _repository;
        private readonly DataMarketDbContext _dataMarketDbContext;
        private readonly ILogger _logger;

        public SavedFilterRepository(ILogger logger, IListInProgressRepository repository)
        {
            _dataMarketDbContext = new DataMarketDbContext();
            _logger = logger;
            _repository = repository;
        }
       
        public async Task<IEnumerable<SavedFilter>> GetByUserId(int id)
        {
            var savedFilters = _dataMarketDbContext.SavedFilters
                .Where(f => f.UserId == id).ToList();
            return savedFilters;
        }

        public async Task<SavedFilter> GetBySavedFilterId(int savedFilterId)
        {
            var savedFilter = _dataMarketDbContext.SavedFilters.Where(sf => sf.SavedFilterId == savedFilterId).FirstOrDefault();

            return savedFilter;
        }

        public async Task DeleteById(int id)
        {
            var savedFilter = _dataMarketDbContext.SavedFilters
                .Where(f => f.SavedFilterId == id).FirstOrDefault();
            _dataMarketDbContext.SavedFilters.Remove(savedFilter);
            _dataMarketDbContext.SaveChanges();

            var query = "Drop table SequenceForList_" + id.ToString();

            SqlConnection objConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DataMarketConnectionString"].ConnectionString);
            {
                objConnection.Open();

                using (SqlCommand cmd = new SqlCommand(query, objConnection))
                {
                    cmd.ExecuteNonQuery();
                }
                objConnection.Close();
            }
        }

        public async Task AddSavedFilter(SavedFilter savedFilter)
        {
            savedFilter.SavedIds = new List<SavedIds>();
            savedFilter.StatusId = ListStatus.Queued;
            savedFilter.CreatedDate = DateTime.Now;

            var listInProgress = await _repository.GetListInProgress(savedFilter.UserId);
            foreach(var listItem in listInProgress)
            {
                var savedIds = new SavedIds
                {
                    GroupId = listItem.GroupId,
                    FilterId = listItem.FilterId,
                    FilterValueId = listItem.FilterValueId,
                    SavedFilter = savedFilter
                };
                savedFilter.SavedIds.Add(savedIds);
            }
            _dataMarketDbContext.SavedFilters.Add(savedFilter);
            _dataMarketDbContext.SaveChanges();
        }

        public void Dispose()
        {
            if (_dataMarketDbContext != null)
            {
                _dataMarketDbContext.Dispose();
            }
        }
    }
}
