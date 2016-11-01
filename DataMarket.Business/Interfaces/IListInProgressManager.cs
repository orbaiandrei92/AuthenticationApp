using DataMarket.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMarket.Business
{
    public interface IListInProgressManager: IDisposable
    {
        Task<IEnumerable<ListInProgressItemDto>> GetListInProgress(int userId);
        void DeleteListInProgressItem(int userId, int filterValueId);
    }
}
