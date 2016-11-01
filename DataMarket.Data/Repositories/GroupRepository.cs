using DataMarket.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataMarket.Data
{
    public class GroupRepository : IGroupRepository, IDisposable
    {

        private readonly DataMarketConfigurationDbContext _dataMarketConfigurationDbContext;
        private readonly ILogger _logger;

        public GroupRepository(ILogger logger)
        {
            this._dataMarketConfigurationDbContext = new DataMarketConfigurationDbContext();
            this._logger = logger;
        }

        public async Task<Group> GetGroupByGroupId(int groupId)
        {
            var groupName = _dataMarketConfigurationDbContext.Groups.Where(g => g.GroupId == groupId).FirstOrDefault();
            _logger.Log(LoggingLevel.Info, "The group retrieved from the database : {0}", groupName);
            return groupName;
        }

        public async Task<IEnumerable<Group>> GetAll()
        {
            var groups = _dataMarketConfigurationDbContext.Groups;
            _logger.Log(LoggingLevel.Info, "Number of groups retrieved from the database : {0}", groups.ToList().Count);
            return groups;
        }

        public async Task<IEnumerable<Group>> GetByParent(int valParent)
        {
            var groupsByParent = _dataMarketConfigurationDbContext.Groups.Where(g => g.ParentGroup == valParent).ToList();
            _logger.Log(LoggingLevel.Info, "Number of groups retrieved from the database : {0}", groupsByParent.Count);
            return groupsByParent;
        }

        public async Task<IEnumerable<Group>> GetGroupsByGroupsId(List<int> groupsId)
        {
            var groups = _dataMarketConfigurationDbContext.Groups.Where(g => groupsId.Contains(g.GroupId)).ToList();
            _logger.Log(LoggingLevel.Info, "Number of groups Name retrieved from the database :{0}", groups.Count);
            return groups;
        }

        public void Dispose()
        {
            if (_dataMarketConfigurationDbContext != null)
            {
                _dataMarketConfigurationDbContext.Dispose();
            }
        }

    }
}
