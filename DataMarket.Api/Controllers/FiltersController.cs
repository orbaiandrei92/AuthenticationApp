using DataMarket.Business;
using DataMarket.Infrastructure;
using DataMarket.DTOs;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using System.Web.Http.Cors;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace DataMarket.Api
{
    [RoutePrefix("api/filters")]
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


        [EnableCors("*", "*", "*")]
        [Route("SaveDataMarketListInProgres")]
        [HttpPost]
        public async Task<int> SaveDataMarketListInProgres()
        {
            //var response = await GetRequestValues<SelectedFilter>();
            string requestStr = await this.Request.Content.ReadAsStringAsync();
            var a = System.Web.HttpUtility.ParseQueryString(requestStr);

            var response = new SelectedFilter
            {
                filterId = int.Parse(a.Get("filterId")),
                filterValue = bool.Parse(a.Get("filterValue"))
            };


            var check = response.filterValue;
            var FilterValueId = response.filterId;
            int _FilterId=6;
            int _GroupId;
            int _datamartId;

            using (SqlConnection con = new SqlConnection("Data Source=192.168.33.201;Initial Catalog=DataMarketConfiguration;User ID=msalagean;Password=pttsr8Ly"))
            {


                con.Open();
                //string queryFilterId = string.Format("select FilterId from FilterValues where FilterValueId={0}", FilterValueId);
                //SqlCommand com = new SqlCommand(queryFilterId, con);
                //_FilterId = Convert.ToInt32(com.ExecuteScalar());
                

                string queryGroupId = string.Format("select GroupId from Filters where FilterId='{0}'", _FilterId);
                SqlCommand com1 = new SqlCommand(queryGroupId, con);
                _GroupId = Convert.ToInt32(com1.ExecuteScalar());
                

                string querydatamartId = string.Format("select DatamartId from Groups where GroupId={0}", _GroupId);
                SqlCommand com2 = new SqlCommand(querydatamartId, con);
                _datamartId = Convert.ToInt32(com2.ExecuteScalar());

              


            }
            if (check)
            {

                using (SqlConnection con = new SqlConnection("Data Source=192.168.33.201;Initial Catalog=DataMarketListInProgress;User ID=msalagean;Password=pttsr8Ly"))
                {
                    con.Open();
                    string tableName = string.Format("ListInProgress_{0}", /*userId,*/  _datamartId);
                    StringBuilder query = new StringBuilder("If not exists ");
                    query.AppendFormat("select 1 from DataMarketListInProgrees.TABLES where table_name ='{0}') CREATE TABLE '{0}' (FilterValueId int NOT NULL, FilterId int NOT NULL, GroupId int NOT NULL,AddedDate date )", tableName);
                    query.AppendFormat("INSERT INTO '{0}' (FilterValueId, FilterId, GroupId, AddedDate) ", tableName);
                    var AddedDate = DateTime.Now.Date;
                    query.AppendFormat("VALUES ({0}" + FilterValueId + "','" + _FilterId + "','" + _GroupId + "','" + AddedDate + "')",tableName);
                    SqlCommand com = new SqlCommand(query.ToString(), con);
                    com.ExecuteNonQuery();
                }
            }
            //else
            //{

            // }


            return response.filterId;

        }

        [Route("getCount")]
        [HttpGet]

        public async Task<int> getCount()
        {
            return 2;
        }


        //[Route("getCount")]
        //[HttpGet]

        //public async Task<int> getCount(int userId, int datamartId/*, Dictionary<int, int> dictionaryFilter*/)
        //{
        //    return 10;


        //      //var worker = new BackgroundWorker();
        //      //worker.DoWork += (int UID,int datamartId, Dictionary<int, int> dictionaryFilter) => savetemporalylist(int UID, int datamartId, Dictionary < int, int > dictionaryFilter);
        //      //worker.RunWorkerAsync();


        //var FilterId=("select FilterId From FilterValues where FilterValueId={0}", FilterValueId);
        //var GroupId=("select GroupId From Filters where FilterId={0}",FilterId);


        //}



        //[Route("GetCount")]
        //[HttpGet]

        //public async Task<IEnumerable<FilterDto>> GetCount(int datamartId, Dictionary<int,int> dictionaryFilter)
        //{
        //    _logger.Log(LoggingLevel.Info, "GetAvailable");
        //    string dmName = await _datamartManager.GetNameById(datamartId);

        //    StringBuilder query = new StringBuilder("SELECT COUNT(*) FROM ");

        //    query.AppendFormat("{0}.dbo.FilterValues AS FV WHERE", dmName);
        //    foreach (var pair in dictionaryFilter)
        //    {
        //        query.AppendFormat(" (FV.FilterId = {0} AND FV.FilterValueId = {1}) ", pair.Key, pair.Value);
        //    }



        //    var data = await _datamartManager.GetCount( , );

        //    return count;
        //}




        [Route("datamarts")]
        [HttpGet]
        public async Task<IEnumerable<DatamartDto>> GetAvailableDatamarts()
        {
            _logger.Log(LoggingLevel.Info, "GetAvailableDatamarts started");
            var allDatamarts = await _datamartManager.GetAllDatamarts();
            return allDatamarts;
            
        }

        [Route("bydatamarts/{id}")]
        [HttpPost]
        public async Task<DatamartDto> GetAvailableDatamartById([FromBody]int id)
        {
            _logger.Log(LoggingLevel.Info, "GetAvailableDatamartById started");
            var datamart = await _datamartManager.GetDatamartById(id);
            return datamart;
        }

        [Route("groups")]
        [HttpGet]
        public async Task<IEnumerable<GroupDto>> GetGroupsByDatamarts()
        {
            _logger.Log(LoggingLevel.Info, "GetGroupByDatamarts started");

            //await
            return new List<GroupDto>();
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

        [Route("GetByGroupId")]
        [HttpGet]
        public async Task<IEnumerable<FilterDto>> GetFiltersByGroupId(int groupId)
        {
            _logger.Log(LoggingLevel.Info, "GetFiltersByGroupId started");
            try
            {
                return await _filterManager.GetFiltersByGroupId(groupId);
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
            //return JsonConvert.DeserializeObject<T>(requestStr);

            return requestStr;
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


public class SelectedFilter
{
    public int filterId { get; set; }
    public bool filterValue { get; set; }

}
