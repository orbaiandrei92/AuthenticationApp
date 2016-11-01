using System.Collections.Generic;
using System.Threading.Tasks;
using DataMarket.DTOs;
using DataMarket.Infrastructure;
using DataMarket.Data;
using System;

namespace DataMarket.Business
{
    public class SavedFilterManager : ISavedFilterManager
    {
        private readonly ILogger _logger;
        private readonly IMapperResolver _mapper;
        private readonly ISavedFilterRepository _repository;

        public SavedFilterManager(ILogger logger, IMapperResolver mapperResolver, ISavedFilterRepository savedFilterRepository)
        {
            this._logger = logger;
            this._mapper = mapperResolver;
            this._repository = savedFilterRepository;
        }

        public async Task AddSavedFilter(SavedFilterDto savedFilterDto)
        {
            var savedFilter = _mapper.Map<SavedFilter>(savedFilterDto);
            await _repository.AddSavedFilter(savedFilter);
        }

        public async Task DeleteSavedFilterById(int id)
        {
           await _repository.DeleteById(id);
        }

        public async Task<IEnumerable<SavedFilterWithStatusDto>> GetSavedFiltersByUserId(int id)
        {
            var allSavedFilters = await _repository.GetByUserId(id);
            var allSavedFiltersDto = _mapper.Map<IEnumerable<SavedFilterWithStatusDto>>(allSavedFilters);
            return allSavedFiltersDto;
        }

        public async Task<SavedFilterDto> GetSavedFilterBySavedFilterId(int id)
        {
            var savedFilter = await _repository.GetBySavedFilterId(id);
            var savedFilterDto = _mapper.Map<SavedFilterDto>(savedFilter);
            return savedFilterDto;
        }

        public void Dispose()
        {
            if (_repository != null)
                _repository.Dispose();
        }
    }
}
