using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMarket.Data
{
    public interface IFilterRepository
    {

        Task<IEnumerable<Filter>> GetAll();

        Task<Filter> GetById(int id);

        Task<IEnumerable<Filter>> GetByGroupId(int groupId);

        Task<IEnumerable<Filter>> GetByDatamartId(int dataMartId);

        Task<IEnumerable<Filter>> GetFiltersByFiltersId(List<int> filters);


        void Dispose();

    }
}
