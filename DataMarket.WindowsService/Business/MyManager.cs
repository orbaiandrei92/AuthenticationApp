using DataMarket.WindowsService.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMarket.WindowsService
{
    public class MyManager
    {
        private MyRepository _repo;
        private bool _mycommand;
        private int myCount;

        public MyManager()
        {
            _repo = new MyRepository();
        }

        public void SetNewStatus(int listId, string myStatus)
        {
            var myStat = _repo.GetStatusIdByStatusName(myStatus);

            _repo.SaveNewStatusId(listId, myStat);
        }

        public string FiltersToSelect(int listId)
        {
            var myIds = _repo.GetSavedIds(listId);
            var filterValues = _repo.GetFilterValuesByListOfFilterValueIds(myIds.Select(s => s.FilterValueId).ToList());

            List<string> myFiltersList = new List<string>();

            foreach (FilterValue filterValue in filterValues)
            {
                if (filterValue.Filter.Group.ParentGroup == 0)
                {
                    myFiltersList.Add(filterValue.FilterValueName);

                }
                else myFiltersList.Add(filterValue.Filter.FilterName);
            }

            return string.Join(",", myFiltersList.Distinct());
        }

        public string CreateTheQuery(int listId, string selectString, string tableName)
        {            

            StringBuilder query = new StringBuilder();//The final query
            StringBuilder querySelect = new StringBuilder();
            StringBuilder queryFrom = new StringBuilder();
            StringBuilder queryJoin = new StringBuilder();
            StringBuilder queryValues = new StringBuilder();
            string op = null;
            string andOp = " AND ";//May change later!

            var myIds = _repo.GetSavedIds(listId);

            var datamart = _repo.GetCurrentDatamart(myIds.Select(s => s.GroupId).First());
            var groups = _repo.GetGroupsByListOfGroupIds(myIds.Select(s => s.GroupId).Distinct().ToList());
            var filters = _repo.GetFiltersByListOfFilterIds(myIds.Select(s => s.FilterId).Distinct().ToList());
            var filterValues = _repo.GetFilterValuesByListOfFilterValueIds(myIds.Select(s => s.FilterValueId).ToList());

            //query.AppendFormat(TheSelect(datamart, selectString, "INTO " + tableName).ToString());
            querySelect.AppendFormat(TheSelect(selectString, "INTO " + tableName).ToString());
            queryFrom.AppendFormat(TheFrom(datamart).ToString());

            int i = 0;
            int j = 0;
            foreach (Group group in groups)
            {

                queryJoin.AppendFormat(TheJoin(datamart, group, i).ToString());

                if (i != 0)
                {
                    queryValues.AppendFormat(andOp);
                }

                queryValues.AppendFormat("(");
                foreach (FilterValue filterValue in filterValues)
                {
                    if (group.GroupId == filterValue.Filter.Group.GroupId)
                    {
                        if (j != 0)
                        {
                            queryValues.AppendFormat(String.Concat(" ", op));
                        }

                        op = filterValue.Filter.Group.Operator;

                        queryValues.AppendFormat(TheValues(filterValue, i).ToString());

                        j++;
                    }
                }
                queryValues.AppendFormat(")");

                j = 0;
                i++;
            }

            query.AppendFormat(querySelect.ToString());
            query.AppendFormat(queryFrom.ToString());
            query.AppendFormat(queryJoin.ToString());
            query.AppendFormat(" WHERE ");
            query.AppendFormat(queryValues.ToString());

            _repo.SaveQuery(listId, queryFrom.ToString() + queryJoin.ToString() + " WHERE " + queryValues.ToString());

            return query.ToString();
        }

        public void GetEachCount(int listId, string tableName)
        {
            StringBuilder query = new StringBuilder();//The final query

            var myIds = _repo.GetSavedIds(listId);

            var filterValues = _repo.GetFilterValuesByListOfFilterValueIds(myIds.Select(s => s.FilterValueId).ToList());

            foreach (FilterValue filterValue in filterValues)
            {
                if (filterValue.Filter.Group.ParentGroup == 0)
                {
                    query.AppendFormat("SELECT COUNT(*) FROM " + tableName + " AS result WHERE " + filterValue.FilterValueName + " = '" + filterValue.Value + "'");
                }
                else query.AppendFormat("SELECT COUNT(*) FROM " + tableName + " AS result WHERE " + filterValue.Filter.FilterName + " = '" + filterValue.FilterValueName + "'");
                _mycommand = true;
                ExecuteQuery(query.ToString(), _mycommand);
                var mySavedId = _repo.GetSavedIdByFilterValueId(listId, filterValue.FilterValueId);
                _repo.AddCount(mySavedId, myCount);
                query.Clear();
            }
              
            var dropTable = @"if exists(select 1 from sys.TABLES where NAME = '" + tableName + "') DROP TABLE " + tableName;
            _mycommand = false;
            ExecuteQuery(dropTable, _mycommand);
        }       

        public void ExecuteQuery(string query, bool option)
        {
            

            SqlConnection objConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DataMarketConnectionString"].ConnectionString);
            {
                objConnection.Open();

                using (SqlCommand cmd = new SqlCommand(query, objConnection))
                {
                    switch(option)
                    {
                        case true:
                            myCount = Convert.ToInt32(cmd.ExecuteScalar());                           
                            break;
                        case false:
                            cmd.ExecuteNonQuery();                            
                            break;
                    }                  
                }
                objConnection.Close();
            }          
        }

        #region The StringBuilder Methods        

        public StringBuilder TheSelect(string filterEnums, string newTable)
        {
            StringBuilder query = new StringBuilder();

            query.AppendFormat("SELECT {0} {1} ", filterEnums, newTable);

            return query;
        }

        public StringBuilder TheFrom(Datamart datamart)
        {
            StringBuilder query = new StringBuilder();

            query.AppendFormat("FROM {0}.{1} AS F", datamart.DatamartName, datamart.FactName);

            return query;
        }

        public StringBuilder TheJoin(Datamart datamart, Group group, int nr)
        {
            StringBuilder query = new StringBuilder();

            query.AppendFormat(" LEFT JOIN {0}.{1} AS G{2} ON G{2}.id = F.{3}", datamart.DatamartName, group.GroupName, nr, group.FactKey);

            return query;
        }

        public StringBuilder TheValues(FilterValue filterValue, int nr)
        {
            StringBuilder query = new StringBuilder();

            if(filterValue.Filter.Group.ParentGroup == 0)
            {
                query.AppendFormat(" G{0}.{1} = '{2}'", nr, filterValue.FilterValueName, filterValue.Value);
            }
            else query.AppendFormat(" G{0}.{1} = '{2}'", nr, filterValue.Filter.FilterName, filterValue.FilterValueName);

            return query;
        }

        #endregion

    }
}
