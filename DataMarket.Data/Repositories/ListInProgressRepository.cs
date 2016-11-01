using DataMarket.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMarket.Data
{
    public class ListInProgressRepository : IListInProgressRepository
    {
        private readonly ILogger _logger;
        private readonly IGroupRepository _groupRepository;
        private readonly IFilterRepository _filterRepository;
        private readonly IFilterValueRepository _filterValueRepository;

        public ListInProgressRepository(ILogger logger, IGroupRepository groupRepository, IFilterRepository filterRepository, IFilterValueRepository filterValueRepository)
        {
            _logger = logger;
            _groupRepository = groupRepository;
            _filterRepository = filterRepository;
            _filterValueRepository = filterValueRepository;
        }

        public void DeleteItemFromListInProgress(int userId, int filterValueId)
        {
            var tableName = string.Format("ListInProgress_{0}", userId);
            var connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            var connection = new SqlConnection(connectionString);
            connection.Open();
            var queryString = string.Format("DELETE FROM {0} WHERE FilterValueId={1}", tableName, filterValueId);
            var command = new SqlCommand(queryString, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public async Task<IEnumerable<ListInProgressItem>> GetListInProgress(int userId)
        {
            var tableName = string.Format("ListInProgress_{0}", userId);

            var connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            var connetionStringDMC = ConfigurationManager.AppSettings["ConnectionStringDMC"];

            var connection = new SqlConnection(connectionString);
            var connectionDMC = new SqlConnection(connetionStringDMC);

            var queryString = string.Format("SELECT * FROM {0}", tableName);
            var command = new SqlCommand(queryString, connection);

            connection.Open();
            connectionDMC.Open();
            var dataReader = await command.ExecuteReaderAsync();
            var listInProgress = new List<ListInProgressItem>();
            while (dataReader.Read())
            {
                var listItemId = int.Parse(dataReader["ListItemId"].ToString());
                var groupId = int.Parse(dataReader["GroupId"].ToString());
                var filterId = int.Parse(dataReader["FilterId"].ToString());
                var filterValueId = int.Parse(dataReader["FilterValueId"].ToString());

                var group = await _groupRepository.GetGroupByGroupId(groupId);
                var filter= await _filterRepository.GetById(filterId);
                var filterValue = await _filterValueRepository.GetFilterValuesByFilterValueId(filterValueId);

                var listItem = new ListInProgressItem
                {
                    ListItemId = listItemId,
                    GroupId = groupId,
                    FilterId = filterId,
                    FilterValueId = filterValueId,
                    GroupName = group.GroupName,
                    FilterName = filter.FilterName,
                    FilterValueName = filterValue.FilterValueName
                };
                listInProgress.Add(listItem);
            }
            connection.Close();
            connectionDMC.Close();
            return listInProgress;
        }
    }
}
