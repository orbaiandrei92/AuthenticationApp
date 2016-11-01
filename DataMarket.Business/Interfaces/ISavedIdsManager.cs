using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataMarket.DTOs;

namespace DataMarket.Business
{
    public interface ISavedIdsManager : IDisposable
    {
        Task<IEnumerable<SavedIdsDto>> GetSavedIdsBySavedFilterId(int savedFilterId);
    }
}
