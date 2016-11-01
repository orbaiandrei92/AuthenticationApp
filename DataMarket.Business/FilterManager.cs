using DataMarket.Infrastructure;
using DataMarket.Data;
using DataMarket.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace DataMarket.Business
{
    public class FilterManager : IFilterManager, IDisposable
    {

        private readonly ILogger _logger;
        private readonly IMapperResolver _mapper;
        private readonly IFilterRepository _repository;
        private readonly IGroupRepository _groupRepository;
        private readonly IFilterValueRepository _filterValueRepository;
        private readonly IDatamartRepository _datamartRepository;

        public FilterManager(ILogger logger, IMapperResolver mapper, IDatamartRepository datamartRepository, IFilterRepository filterRepository, IGroupRepository groupRepository, IFilterValueRepository filterValueRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = filterRepository;
            _groupRepository = groupRepository;
            _filterValueRepository = filterValueRepository;
            _datamartRepository = datamartRepository;
        }

        public void Dispose()
        {
            if (_repository != null)
                _repository.Dispose();
        }

        public async Task<IEnumerable<FilterDto>> GetFilters()
        {
            var filters = await _repository.GetAll();
            var filtersDto = _mapper.Map<IEnumerable<FilterDto>>(filters);
            return filtersDto;
        }

        public async Task<IEnumerable<FilterDto>> GetFiltersByGroupId(int groupId)
        {
            var filters = await _repository.GetByGroupId(groupId);
            var filtersDto = _mapper.Map<IEnumerable<FilterDto>>(filters);
            return filtersDto;
        }

        public async Task<IEnumerable<GroupDto>> GetGroups()
        {
            var groups = await _groupRepository.GetAll();
            var groupsDto = _mapper.Map<IEnumerable<GroupDto>>(groups);
            return groupsDto;
        }

        public async Task<IEnumerable<GroupDto>> GetGroupsByDatamartId(int datamartId, int valParent)
        {
            var allGroupsByParent = await _groupRepository.GetByParent(valParent);
            var groups = allGroupsByParent.Where(g => g.DatamartId == datamartId);
            var groupsDto = _mapper.Map<IEnumerable<GroupDto>>(groups);
            return groupsDto;
        }

        public async Task<IEnumerable<FilterDto>> GetFiltersByDatamartId(int datamartId, int valParent)
        {
            var allFilters = await _repository.GetByDatamartId(datamartId);
            var filters = allFilters.Where(f => f.Group.ParentGroup == valParent);
            var filtersDto = _mapper.Map<IEnumerable<FilterDto>>(filters);
            return filtersDto;
        }

        public async Task<IEnumerable<FilterValueDto>> GetFilterValuesByDatamartId(int datamartId, int valParent)
        {
            var allFilterValues = await _filterValueRepository.GetByDatamartId(datamartId);
            var filterValues = allFilterValues.Where(f => f.Filters.Group.ParentGroup == valParent);
            var filterValuesDto = _mapper.Map<IEnumerable<FilterValueDto>>(filterValues);
            return filterValuesDto;
        }

        public async Task<IEnumerable<FilterValueDto>> GetFilterValuesByFiltersId(int filterId)
        {
            var filterValues = await _filterValueRepository.GetFilterValuesByFilterId(filterId);
            var filterValuesDto = _mapper.Map<IEnumerable<FilterValueDto>>(filterValues);
            return filterValuesDto;
        }

        public async Task<IEnumerable<FilterValueDto>> GetFilterValuesByFilterValueIds(List<int> filterValueIds)
        {
            var filterValues = await _filterValueRepository.GetFilterValuesByFilterValueIds(filterValueIds);
            var filterValuesDto = _mapper.Map<IEnumerable<FilterValueDto>>(filterValues);
            return filterValuesDto;
        }

        public async Task<IEnumerable<FilterValueDto>> GetFiltersIdByFilterValueIds(List<int> filterValueIds)
        {
            var filters = await _filterValueRepository.GetFilterValuesByFilterValueIds(filterValueIds);
            var filtersDto = _mapper.Map<IEnumerable<FilterValueDto>>(filters);
            return filtersDto;
        }

        public async Task<IEnumerable<FilterDto>> GetFilterByFilterId(List<int> filters)
        {
            var filtersName = await _repository.GetFiltersByFiltersId(filters);
            var filtersNameDto = _mapper.Map<IEnumerable<FilterDto>>(filtersName);
            return filtersNameDto;
        }

        public async Task<IEnumerable<GroupDto>> GetGroupsByGroupsId(List<int> groupsId)
        {
            var groups = await _groupRepository.GetGroupsByGroupsId(groupsId);
            var groupsDto = _mapper.Map<IEnumerable<GroupDto>>(groups);
            return groupsDto;
        }

        public async Task<DatamartDto> GetDatamartByDatamartId(int datamartsId)
        {
            var datamartName = await _datamartRepository.GetDatamartsByDatamartsId(datamartsId);
            var datamartIdDto = _mapper.Map<DatamartDto>(datamartName);
            return datamartIdDto;
        }
    }
}
