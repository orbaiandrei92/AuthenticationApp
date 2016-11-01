using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMarket.Data
{
    public interface ITestRepository
    {

        IEnumerable<TestEntity> GetAll();

        IEnumerable<Filter> GetFiltersByGroupId(int groupId);

        Task<IEnumerable<Filter>> GetFiltersByGroupIdAsync(int groupId);

        void Dispose();

    }
}
