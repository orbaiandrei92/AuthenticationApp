using DataMarket.Business;
using DataMarket.Infrastructure;
using DataMarket.DTOs;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data.SqlClient;
using System.Text;
using Newtonsoft.Json;
using System.Web.Http.Cors;
using System.Web;
using System.Configuration;

namespace DataMarketResources.Api
{

    [RoutePrefix("api/filters")]
    [Authorize]
    public class FiltersController : ApiController
    {

        private readonly ILogger _logger;
        private readonly IFilterManager _filterManager;
        private readonly IDatamartManager _datamartManager;

        public FiltersController(ILogger logger, IFilterManager filterManager, IDatamartManager datamartManager)
        {
            _logger = logger;
            _filterManager = filterManager;
            _datamartManager = datamartManager;

        }

        [Route("datamarts")]
        [HttpGet]
        public async Task<IEnumerable<DatamartDto>> GetAvailableDatamarts()
        {
            _logger.Log(LoggingLevel.Info, "GetAvailableDatamarts started");
            try
            {
                return await _datamartManager.GetAllDatamarts();
            }
            catch (Exception ex)
            {
                _logger.Log(LoggingLevel.Error, "There was an error : ", ex);
            }
            return new List<DatamartDto>();
        }

        [Route("datamarts/{id}")]
        [HttpGet]
        public async Task<DatamartDto> GetAvailableDatamartById(int id)
        {
            _logger.Log(LoggingLevel.Info, "GetAvailableDatamartById started");
            try
            {
                return await _datamartManager.GetDatamartById(id);
            }
            catch (Exception ex)
            {
                _logger.Log(LoggingLevel.Error, "There was an error : ", ex);
            }
            return new DatamartDto();
        }

        [Route("groups")]
        [HttpGet]
        public async Task<IEnumerable<GroupDto>> GetGroups()
        {
            _logger.Log(LoggingLevel.Info, "GetGroups started");
            try
            {
                return await _filterManager.GetGroups();
            }
            catch (Exception ex)
            {
                _logger.Log(LoggingLevel.Error, "There was an error : ", ex);
            }
            return new List<GroupDto>();
        }

        [Route("groups/{id}/{parent}")]
        [HttpGet]
        public async Task<IEnumerable<GroupDto>> GetGroupsByDatamartId(int id, int parent)
        {
            _logger.Log(LoggingLevel.Info, "GetGroupsByDatamartId started");
            var groups = await _filterManager.GetGroupsByDatamartId(id, parent);
            return groups;
        }





        [Route("filters")]
        [HttpGet]
        public async Task<IEnumerable<FilterDto>> GetFilters()
        {
            _logger.Log(LoggingLevel.Info, "GetFilters started");
            try
            {
                return await _filterManager.GetFilters();
            }
            catch (Exception ex)
            {
                _logger.Log(LoggingLevel.Error, "There was an error : ", ex);
            }
            return new List<FilterDto>();
        }

        [Route("filterValues/{id}")]
        [HttpGet]
        public async Task<IEnumerable<FilterValueDto>> GetFilterValuesByFilterId(int id)
        {
            _logger.Log(LoggingLevel.Info, "GetFilterValuesByFilterId");
            try
            {
                return await _filterManager.GetFilterValuesByFiltersId(id);
            }
            catch (Exception ex)
            {
                _logger.Log(LoggingLevel.Error, "There was an error : ", ex);
            }
            return new List<FilterValueDto>();
        }

        [Route("filters/{id}")]
        [HttpGet]
        public async Task<IEnumerable<FilterDto>> GetFiltersByGroupId(int id)
        {
            _logger.Log(LoggingLevel.Info, "GetFiltersByGroupId started");
            try
            {
                return await _filterManager.GetFiltersByGroupId(id);
            }
            catch (Exception ex)
            {
                _logger.Log(LoggingLevel.Error, "There was an error : ", ex);
            }
            return new List<FilterDto>();
        }

        [Route("counts")]
        [HttpGet]
        public async Task<int> GetCountBySelectedFilters()
        {
            _logger.Log(LoggingLevel.Info, "GetCountBySelectedFilters started");

            //await
            return 20;
        }

        [Route("bydatamart/{id}/{parent}")]
        [HttpGet]
        public async Task<IEnumerable<FilterDto>> GetFiltersByDatamartId(int id, int parent)
        {
            _logger.Log(LoggingLevel.Info, "GetFiltersByDatamartId started");
            try
            {
                //if (Request.Content == null)
                //{
                //    //
                //}
                //var content = await GetRequestValues();
                //var id = int.Parse(content);


                return await _filterManager.GetFiltersByDatamartId(id, parent);
            }
            catch (Exception ex)
            {
                _logger.Log(LoggingLevel.Error, "There was an error : ", ex);
            }
            return new List<FilterDto>();
        }

        [Route("filterValues/{id}/{parent}")]
        [HttpGet]
        public async Task<IEnumerable<FilterValueDto>> GetFilterValuesByDatamartId(int id, int parent)
        {
            _logger.Log(LoggingLevel.Info, "GetFilterValuesByDatamartId started");
            var filterValues = await _filterManager.GetFilterValuesByDatamartId(id, parent);
            return filterValues;
        }

        [Route("save")]
        [HttpPost]
        public async Task<HttpResponseMessage> SaveFilters()
        {
            HttpResponseMessage result;
            _logger.Log(LoggingLevel.Info, "SaveFilters started.");
            try
            {
                if (this.Request.Content == null)
                {
                    _logger.Log(LoggingLevel.Warn, "Request is empty");
                    return this.Request.CreateErrorResponse(HttpStatusCode.PreconditionFailed, "Request content is empty");
                }

                var content = await GetRequestValues();
                if (content == null)
                {
                    _logger.Log(LoggingLevel.Warn, "Request content is empty");
                    return this.Request.CreateErrorResponse(HttpStatusCode.PreconditionFailed, "Request content is empty");
                }

                ProcessRequest(content);

                _logger.Log(LoggingLevel.Info, "Success : {0}", content);

                result = this.Request.CreateResponse(HttpStatusCode.OK, content);

            }
            catch (Exception ex)
            {
                _logger.Log(LoggingLevel.Error, ex.Message, ex);
                result = this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

            return result;
        }

        private void ProcessRequest(string content)
        {
            //do someworkhere
        }

        /// <summary>
        /// Get the request
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetRequestValues()
        {
            string requestStr = await this.Request.Content.ReadAsStringAsync();
            return requestStr;
            //return JsonConvert.DeserializeObject<T>(requestStr);
        }







        //public async Task<IEnumerable<FilterValueDto>> GetFiltersByFilterValuesId(int valuesId)
        //{
        //    _logger.Log(LoggingLevel.Info, "GetFiltersByilterValuesId started");
        //    try
        //    {
        //        return await _filterValueManager.GetFiltersByFilterValuesId(valuesId);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Log(LoggingLevel.Error, "There was an error : ", ex);
        //    }
        //    return new List<FilterValueDto>();
        //}

            



        [EnableCors("*", "*", "*")]
        [Route("getCountAndSaveDataMarketListInProgres")]
        [HttpPost]
        public async Task<int> getCountAndSaveDataMarketListInProgres(Request selectedFilters)
        {

            int count;
            int userId = selectedFilters.userId;
            List<int> _selectedFiltersVal = selectedFilters.selectedfilters;
            


            IEnumerable<FilterValueDto> filterValues = await _filterManager.GetFilterValuesByFilterValueIds(_selectedFiltersVal);
            IEnumerable<FilterDto> filters = await _filterManager.GetFilterByFilterId(filterValues.Select(f => f.FilterId).ToList());
            IEnumerable<GroupDto> groups = await _filterManager.GetGroupsByGroupsId(filters.Select(g => g.GroupId).ToList());
            DatamartDto datamart = await _filterManager.GetDatamartByDatamartId(groups.First().DatamartId);


            StringBuilder query = new StringBuilder();
            StringBuilder whereClause = new StringBuilder();
            query.AppendFormat("SELECT COUNT(Sequence) FROM");
            query.AppendFormat(" {0}.{1} AS F", datamart.DatamartName, datamart.FactName);
            int i = 0;
            foreach (GroupDto item in groups)
            {

                query.AppendFormat(" LEFT JOIN {0}.{1} AS G{2} ON G{2}.id = F.{3}", datamart.DatamartName, item.GroupName, i, item.FactKey);

                var currentFilterValues = from fv in filterValues
                                          join f in filters on fv.FilterId equals f.FilterId
                                          where f.GroupId == item.GroupId
                                          select new
                                          {
                                              fv.FilterValueName,
                                              f.FilterName
                                          };

                var operation = item.Operator;
                StringBuilder groupWhereClause = new StringBuilder();

                foreach (var filterValue in currentFilterValues)
                {
                    if (!string.IsNullOrEmpty(groupWhereClause.ToString()))
                    {
                        groupWhereClause.AppendFormat(" {0}", item.Operator);
                    }

                    if (item.ParentGroup == 0)
                    {
                        groupWhereClause.AppendFormat(" G{0}.{1} = '{2}'", i, filterValue.FilterValueName, 1);
                    }
                    else
                    {
                        groupWhereClause.AppendFormat(" G{0}.{1} = '{2}'", i, filterValue.FilterName, filterValue.FilterValueName);
                    }

                }
                if (!string.IsNullOrEmpty(groupWhereClause.ToString()))
                {
                    if (!string.IsNullOrEmpty(whereClause.ToString()))
                    {

                        whereClause.Append(" AND");
                        whereClause.AppendFormat(" (");

                    }

                    whereClause.AppendFormat(" {0}", groupWhereClause.ToString());
                }

                i++;
                whereClause.AppendFormat(" )");



            }



            if (!string.IsNullOrEmpty(whereClause.ToString()))
            {
                query.AppendFormat(" WHERE ( {0}", whereClause.ToString());
            }


             
             SqlConnection countexecute = new SqlConnection(ConfigurationManager.ConnectionStrings["DataMarketConfiguratinConnectionString"].ConnectionString);
            {
                countexecute.Open();
                using (SqlCommand cmd = new SqlCommand(query.ToString(),countexecute))
                {
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            
            string tableName = string.Format("ListInProgress_{0}", userId);

            SqlConnection conListInProgress = new SqlConnection(ConfigurationManager.ConnectionStrings["DataMarketListInProgressConnectionString"].ConnectionString);
            conListInProgress.Open();

            using (SqlCommand cmd = conListInProgress.CreateCommand())
            {
                cmd.CommandText = @"if exists(select 1 from sys.TABLES where NAME = '" + tableName + "') Drop table " + tableName + "";
                cmd.ExecuteNonQuery();
            }

            using (SqlCommand cmd = conListInProgress.CreateCommand())
            {
                cmd.CommandText = @"if not exists(select 1 from sys.TABLES where NAME = '" + tableName + "')  CREATE TABLE " + tableName + "(ListItemId int IDENTITY(1,1) NOT NULL PRIMARY KEY, FilterValueId int NOT NULL, FilterId int NOT NULL, GroupId int NOT NULL, AddedDate date)";
                cmd.ExecuteNonQuery();
                foreach (GroupDto item in groups)
                {

                    var currentFilterValues = from fv in filterValues
                                              join f in filters on fv.FilterId equals f.FilterId
                                              where f.GroupId == item.GroupId
                                              select new
                                              {
                                                  fv.FilterValueId,
                                                  f.FilterId

                                              };
                    foreach (var filterValue in currentFilterValues)
                    {
                        var addedDate = DateTime.Now.Date;
                        cmd.CommandText = "INSERT INTO " + tableName + "(FilterValueId, FilterId, GroupId,AddedDate) VALUES (" + filterValue.FilterValueId + "," + filterValue.FilterId + "," + item.GroupId + ",'" + addedDate + "')";
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            conListInProgress.Close();
         
            return count;
        }



        public async Task<T> GetRequestValues<T>()
        {
            string requestStr = await this.Request.Content.ReadAsStringAsync();
            //  var a= System.Web.HttpUtility.ParseQueryString(requestStr);
            return JsonConvert.DeserializeObject<T>(requestStr);

            //return new SelectedFilter
            //{
            //    filterId = int.Parse(a.Get("filterId")),
            //    filterValue = bool.Parse(a.Get("filterValue"))
            //};
        }

    }
}


public class Filters
{
    public IList<int> selectedFilters { get; set; }
    public int userId { get; set; }
}

public class Request
{
    public int userId { get; set; }
    public List<int> selectedfilters { get; set; }
}




