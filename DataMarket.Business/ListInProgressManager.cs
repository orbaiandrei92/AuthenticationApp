using System.Collections.Generic;
using System.Threading.Tasks;
using DataMarket.DTOs;
using DataMarket.Infrastructure;
using DataMarket.Data;
using System;

namespace DataMarket.Business
{
    public class ListInProgressManager : IListInProgressManager
    {
        private readonly ILogger _logger;
        private readonly IMapperResolver _mapper;
        private readonly IListInProgressRepository _repository;

        public ListInProgressManager(ILogger logger, IMapperResolver mapper, IListInProgressRepository repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<IEnumerable<ListInProgressItemDto>> GetListInProgress(int userId)
        {
            var listInProgress = await _repository.GetListInProgress(userId);
            var listInProgressDto = _mapper.Map<IEnumerable<ListInProgressItemDto>>(listInProgress);
            return listInProgressDto;         
        }

        public void DeleteListInProgressItem(int userId, int filterValueId)
        {
            _repository.DeleteItemFromListInProgress(userId, filterValueId);
        }

        public void Dispose()
        {

        }
    }
}
