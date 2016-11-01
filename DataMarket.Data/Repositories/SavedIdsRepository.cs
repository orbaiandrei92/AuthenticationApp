using DataMarket.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataMarket.Data
{
    public class SavedIdsRepository : ISavedIdsRepository
    {
        private readonly DataMarketDbContext _dataMarketDbContext;
        private readonly ILogger _logger;
        private readonly IGroupRepository _groupRepository;
        private readonly IFilterRepository _filterRepository;
        private readonly IFilterValueRepository _filterValueRepository;

        public SavedIdsRepository(ILogger logger, IGroupRepository groupRepository, IFilterRepository filterRepository, IFilterValueRepository filterValueRepository)
        {
            _dataMarketDbContext = new DataMarketDbContext();
            _logger = logger;
            _groupRepository = groupRepository;
            _filterRepository = filterRepository;
            _filterValueRepository = filterValueRepository;
        }

        public async Task<IEnumerable<SavedIdsToSend>> GetSavedIdsBySavedFilterId(int savedFilterId)
        {
            var savedIds = _dataMarketDbContext.SavedIds
                .Where(f => f.SavedFilterId == savedFilterId).ToList();

            List<SavedIdsToSend> myList = new List<SavedIdsToSend>();

            foreach (var val in savedIds)
            {
                SavedIdsToSend objSevedId = new SavedIdsToSend();
                var group = await _groupRepository.GetGroupByGroupId(val.GroupId);
                var filter = await _filterRepository.GetById(val.FilterId);
                var filterValue = await _filterValueRepository.GetFilterValuesByFilterValueId(val.FilterValueId);

                objSevedId.GroupName = group.DisplayName;
                objSevedId.FilterName = filter.DisplayName;
                objSevedId.FilterValueName = filterValue.DisplayName;
                objSevedId.FilterId = val.FilterId;
                objSevedId.FilterValueId = val.FilterValueId;
                objSevedId.GroupId = val.GroupId;
                objSevedId.SavedId = val.SavedId;
                objSevedId.SavedFilterId = val.SavedFilterId;
                objSevedId.Count = val.Count;

                myList.Add(objSevedId);
            }

            return myList;
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
