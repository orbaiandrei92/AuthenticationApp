using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMarket.Data
{
    public interface ISavedFilterRepository: IDisposable
    {
        Task<IEnumerable<SavedFilter>> GetByUserId(int id);

        Task<SavedFilter> GetBySavedFilterId(int savedFilterId);

        Task DeleteById(int id);

        Task AddSavedFilter(SavedFilter savedFilter);
    }
}
