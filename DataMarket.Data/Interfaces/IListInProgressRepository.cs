using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMarket.Data
{
    public interface IListInProgressRepository
    {
        Task<IEnumerable<ListInProgressItem>> GetListInProgress(int userId);
        void DeleteItemFromListInProgress(int userId, int filterValueId);
    }
}
