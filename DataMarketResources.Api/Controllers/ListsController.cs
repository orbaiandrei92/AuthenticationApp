using DataMarket.Business;
using DataMarket.DTOs;
using DataMarket.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace DataMarketResources.Api.Controllers
{
    [RoutePrefix("api/lists")]
    [Authorize]
    public class ListsController : ApiController
    {
        private readonly ILogger _logger;
        private readonly ISavedFilterManager _savedFilterManager;
        private readonly ISavedIdsManager _savedIdsManager;

        public ListsController(ILogger logger, ISavedFilterManager savedFilterManager, ISavedIdsManager savedIdsManager)
        {
            _logger = logger;
            _savedFilterManager = savedFilterManager;
            _savedIdsManager = savedIdsManager;
        }

        [Route("myList/{id}")]
        [HttpGet]
        public async Task<SavedFilterDto> GetMyList(int id)
        {
            try
            {
                _logger.Log(LoggingLevel.Info, "GetMyList started");
                var savedFilter = await _savedFilterManager.GetSavedFilterBySavedFilterId(id);
                return savedFilter;
            }
            catch (Exception ex)
            {
                _logger.Log(LoggingLevel.Error, "There was an error : ", ex);
            }
            return new SavedFilterDto();
        }

        [Route("myLists/{id}")]
        [HttpGet]
        public async Task<IEnumerable<SavedFilterWithStatusDto>> GetMyLists(int id)
        {
            try
            {
                _logger.Log(LoggingLevel.Info, "GetMyLists started");
                var allSavedFilters = await _savedFilterManager.GetSavedFiltersByUserId(id);
                return allSavedFilters;
            }
            catch (Exception ex)
            {
                _logger.Log(LoggingLevel.Error, "There was an error : ", ex);
            }
            return new List<SavedFilterWithStatusDto>();
        }

        [Route("deleteList")]
        [HttpPost]
        public async Task DeleteList([FromBody]int id)
        {
            try
            {
                _logger.Log(LoggingLevel.Info, "DeleteList started");
                await _savedFilterManager.DeleteSavedFilterById(id);
            }
            catch (Exception ex)
            {
                _logger.Log(LoggingLevel.Error, "There was an error : ", ex);
            }
        }

        [Route("addList")]
        [HttpPost]
        public async Task AddList([FromBody] SavedFilterDto savedFilterDto)
        {
            try
            {
                _logger.Log(LoggingLevel.Info, "AddList started");
                await _savedFilterManager.AddSavedFilter(savedFilterDto);
            }
            catch (Exception ex)
            {
                _logger.Log(LoggingLevel.Error, "There was an error : ", ex);
            }
        }
        [Route("myListDetails/{id}")]
        [HttpGet]
        public Task<IEnumerable<SavedIdsDto>> GetMyListDetails(int id)
        {
            _logger.Log(LoggingLevel.Info, "GetMyListDetails started");
            var myListDetails = _savedIdsManager.GetSavedIdsBySavedFilterId(id);
            return myListDetails;
        }

    }
}
