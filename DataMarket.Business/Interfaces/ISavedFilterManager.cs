using DataMarket.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMarket.Business
{
    public interface ISavedFilterManager: IDisposable
    {
        Task<SavedFilterDto> GetSavedFilterBySavedFilterId(int id);
        Task<IEnumerable<SavedFilterWithStatusDto>> GetSavedFiltersByUserId(int id);
        Task DeleteSavedFilterById(int id);
        Task AddSavedFilter(SavedFilterDto savedFilterDto);
    }
}
