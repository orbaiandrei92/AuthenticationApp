using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMarket.Data
{
    public class TestRepositoryMock : ITestRepository
    {     
           
        public IEnumerable<TestEntity> GetAll()
        {
            return new List<TestEntity>();
        }

        public IEnumerable<Filter> GetFiltersByGroupId(int groupId)
        {
            return new List<Filter>();
        }

        public Task<IEnumerable<Filter>> GetFiltersByGroupIdAsync(int groupId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

    }
}
