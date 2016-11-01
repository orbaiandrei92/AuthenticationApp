using DataMarket.WindowsService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataMarket.WindowsService
{
    public class MyRepository : IDisposable
    {
        private DataMarketEntities _dataMarketContext;
        private DataMarketConfigurationEntities _dataMarketConfigurationContext;

        public MyRepository()
        {
            _dataMarketContext = new DataMarketEntities();
            _dataMarketConfigurationContext = new DataMarketConfigurationEntities();
        }

        public int GetStatusIdByStatusName(string statusName)
        {
            var myStatus = _dataMarketContext.Statuses.Where(s => s.DisplayName == statusName).FirstOrDefault();

            return myStatus.StatusId;
        }

        public void SaveNewStatusId(int listId, int myNewStatus)
        {
            var myList = _dataMarketContext.SavedFilters.Where(s => s.SavedFilterId == listId).FirstOrDefault();

            myList.StatusId = myNewStatus;
            _dataMarketContext.SaveChanges();
        }

        public void SaveQuery(int listId, string query)
        {
            var myList = _dataMarketContext.SavedFilters.Where(s => s.SavedFilterId == listId).FirstOrDefault();

            myList.Query = query;
            _dataMarketContext.SaveChanges();
        }

        public Datamart GetCurrentDatamart(int groupId)
        {
            var myGroup = _dataMarketConfigurationContext.Groups.Where(s => s.GroupId == groupId).FirstOrDefault();
            var myDatamart = _dataMarketConfigurationContext.Datamarts.Where(s => s.DatamartId == myGroup.DatamartId).FirstOrDefault();

            return myDatamart;
        }

        public SavedId GetSavedIdByFilterValueId(int listId, int filterValueId)
        {
            var mySavedId = _dataMarketContext.SavedIds.Where(s => s.SavedFilterId == listId)
                .Where(s => s.FilterValueId == filterValueId).FirstOrDefault();

            return mySavedId;
        }

        public IList<SavedFilter> GetListsByStatus(string myStatus)
        {
            var myList = _dataMarketContext.SavedFilters.Where(sf => sf.Status.DisplayName == myStatus).OrderBy(sf=> sf.CreatedDate).ToList();

            return myList;
        }     

        public IEnumerable<SavedId> GetSavedIds(int listId)
        {
            var myList = _dataMarketContext.SavedIds.Where(s => s.SavedFilterId == listId).ToList();

            return myList;
        }

        public IEnumerable<Group> GetGroupsByListOfGroupIds(List<int> listId) 
        {
            var myList = _dataMarketConfigurationContext.Groups.Where(s => listId.Contains(s.GroupId)).ToList();

            return myList;
        }

        public IEnumerable<Filter> GetFiltersByListOfFilterIds(List<int> listId) 
        {
            var myList = _dataMarketConfigurationContext.Filters.Where(s => listId.Contains(s.FilterId)).ToList();

            return myList;
        }

        public IEnumerable<FilterValue> GetFilterValuesByListOfFilterValueIds(List<int> listId)
        {
            var myList = _dataMarketConfigurationContext.FilterValues.Where(s => listId.Contains(s.FilterValueId)).ToList();

            return myList;
        }        

        public void AddCount(SavedId savedId, int count)
        {
            savedId.Count = count;
            _dataMarketContext.SaveChanges();
        }

        public void Dispose()
        {
            _dataMarketContext.Dispose();
            _dataMarketConfigurationContext.Dispose();
        }
    }
}
