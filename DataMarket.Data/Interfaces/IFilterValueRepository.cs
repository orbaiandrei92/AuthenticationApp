using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMarket.Data
{
    public interface IFilterValueRepository
    {
        Task<IEnumerable<FilterValue>> GetByDatamartId(int dataMartId);
        Task<FilterValue> GetFilterValuesByFilterValueId(int filterValueId);
        Task<IEnumerable<FilterValue>> GetFilterValuesByFilterId(int filterId);
        Task<IEnumerable<FilterValue>> GetFilterValuesByFilterValueIds(List<int> filterValueIds);

        void Dispose();
    }
}
