using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMarket.Data
{
    public interface IGroupRepository
    {
        Task<Group> GetGroupByGroupId(int groupId);

        Task<IEnumerable<Group>> GetAll();

        Task<IEnumerable<Group>> GetByParent(int valParent);

        Task<IEnumerable<Group>> GetGroupsByGroupsId(List<int> groupsId);

        void Dispose();

    }
}
