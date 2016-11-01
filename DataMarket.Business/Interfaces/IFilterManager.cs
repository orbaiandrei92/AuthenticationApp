using DataMarket.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace DataMarket.Business
{
    public interface IFilterManager
    {
        Task<IEnumerable<FilterDto>> GetFilters();
        Task<IEnumerable<FilterDto>> GetFiltersByGroupId(int groupId);
        Task<IEnumerable<GroupDto>> GetGroups();
        Task<IEnumerable<GroupDto>> GetGroupsByDatamartId(int datamartId, int valParent);
        Task<IEnumerable<FilterDto>> GetFiltersByDatamartId(int dataMartId, int valParent);
        Task<IEnumerable<FilterValueDto>> GetFilterValuesByDatamartId(int datamartId, int valParent);
        Task<IEnumerable<FilterValueDto>> GetFilterValuesByFiltersId(int filterId);
        Task<IEnumerable<FilterValueDto>> GetFilterValuesByFilterValueIds(List<int> filterValueIds);
        Task<IEnumerable<FilterDto>> GetFilterByFilterId(List<int> filters);
        Task<IEnumerable<GroupDto>> GetGroupsByGroupsId(List<int> groupsId);
        Task<DatamartDto> GetDatamartByDatamartId(int datamartsId);
    }
}
