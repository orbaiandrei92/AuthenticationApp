using System.Web.Mvc;
using DataMarket.Web.Models;
using DataMarket.Infrastructure;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace DataMarket.Web.Controllers
{
    public class getCountAndSaveDataMarketListInProgresController : Controller
    {

        private readonly ILogger logger;
        private readonly IApiCaller caller;
        private IApiMethods apiMethods;

        public getCountAndSaveDataMarketListInProgresController(ILogger logger, IApiCaller caller, IApiMethods apiMethods)
        {
            this.logger = logger;
            this.caller = caller;
            this.apiMethods = apiMethods;
        }

        [HttpPost]
        public async Task<int> getCount(List<int> selectedFilters)
        {
            var token = (string)Session["token"];
            var userId = (int)Session["userId"];
            var request = new Request(); 
            request.SelectedFilters = selectedFilters;
            request.UserId = userId;
            
            var  countVal = await caller.PostWithAuthorization2<Request,int>(apiMethods.FiltersBaseUrl(), apiMethods.getCountAndSaveDataMarketListInProgresController, userId, request, token);
            Session["countVal"] = countVal;
            return countVal;

        }
        
    }

    public class Request
    {
        public IList<int> SelectedFilters { get; set; }
        public int UserId { get; set; }
    }

    public class Response
    {
        public int CountValue { get; set; }
    }
}