using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMarket.Data
{
    public interface ISavedIdsRepository : IDisposable
    {
        Task<IEnumerable<SavedIdsToSend>> GetSavedIdsBySavedFilterId(int savedFilterId);
    }
}
