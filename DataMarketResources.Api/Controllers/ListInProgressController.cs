using DataMarket.Business;
using DataMarket.DTOs;
using DataMarket.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Linq;

namespace DataMarketResources.Api
{
    [RoutePrefix("api/listInProgress")]
    [Authorize]
    public class ListInProgressController : ApiController
    {
        private readonly ILogger _logger;
        private readonly IListInProgressManager _listInProgressManager;

        public ListInProgressController(ILogger logger, IListInProgressManager listInProgressManager)
        {
            _logger = logger;
            _listInProgressManager = listInProgressManager;
        }

        [Route("userList/{id}")]
        [HttpGet]
        public async Task<IEnumerable<ListInProgressItemDto>> GetListInProgress(int id)
        {
            _logger.Log(LoggingLevel.Info, "GetListInProgress started");
            try
            {
                var listInProgress = await _listInProgressManager.GetListInProgress(id);

                return listInProgress;
            }
            catch (Exception ex)
            {
                _logger.Log(LoggingLevel.Error, "There was an error : ", ex);
            }
            return new List<ListInProgressItemDto>();
        }

        [Route("deleteItem")]
        [HttpPost]
        public void DeleteListInProgressItem(int userId, int filterValueId)
        {
            _logger.Log(LoggingLevel.Info, "DeleteListItem started");
            try
            {
                _listInProgressManager.DeleteListInProgressItem(userId, filterValueId);
            }
            catch (Exception ex)
            {
                _logger.Log(LoggingLevel.Error, "There was an error : ", ex);
            }
        }
    }
}
