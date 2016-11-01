using DataMarket.Data;
using DataMarket.DTOs;
using DataMarket.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMarket.Business
{
    public class SavedIdsManager : ISavedIdsManager
    {
        private readonly ILogger _logger;
        private readonly IMapperResolver _mapper;
        private readonly ISavedIdsRepository _repository;

        public SavedIdsManager(ILogger logger, IMapperResolver mapper, ISavedIdsRepository repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<SavedIdsDto>> GetSavedIdsBySavedFilterId(int savedFilterId)
        {
            var mySavedIds = await _repository.GetSavedIdsBySavedFilterId(savedFilterId);
            var mySavedIdsDto = _mapper.Map<IEnumerable<SavedIdsDto>>(mySavedIds);
            return mySavedIdsDto;
        }

        public void Dispose()
        {
            if (_repository != null)
                _repository.Dispose();
        }
    }
}
