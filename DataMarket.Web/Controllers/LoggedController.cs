using System.Web.Mvc;
using DataMarket.Web.Models;
using DataMarket.Infrastructure;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using DataMarket.DTOs;
using PagedList;
using System.Web.Security;
using System.Net.Mail;
using System.Text;
using System.Net;
using System;
using System.Security.Cryptography;

namespace DataMarket.Web.Controllers
{
    public class LoggedController : Controller
    {
        private readonly ILogger logger;
        private readonly IApiCaller caller;
        private IApiMethods apiMethods;

        public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
                base.OnActionExecuting(filterContext);
            }
        }

        public LoggedController(ILogger logger, IApiCaller caller, IApiMethods apiMethods)
        {
            this.logger = logger;
            this.caller = caller;
            this.apiMethods = apiMethods;
        }

        public async Task<ActionResult> Create()
        {
            var token = Session["token"].ToString();
            var model = await caller.GetWithAuthorization<IEnumerable<DatamartViewModel>>(apiMethods.FiltersBaseUrl(), apiMethods.GetAvailableDatamarts, token);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> MyLists()
        {
            var token = Session["token"].ToString();
            var sortType = Session["sortType"].ToString();
            var id = Session["userId"].ToString();

            var model = await caller.GetWithAuthorization<IEnumerable<ListViewModel>>(apiMethods.ListsBaseUrl(), string.Concat(apiMethods.GetListsByUserId, id), token);

            var displayModel = new List<MyListViewModel>();

            foreach (var val in model)
            {
                var myDatamart = await caller.GetWithAuthorization<DatamartViewModel>(apiMethods.FiltersBaseUrl(), string.Concat(apiMethods.GetAvailableDatamartById, val.Datamart), token);

                var myModel = new MyListViewModel
                {
                    SavedFilterId = val.SavedFilterId,
                    ListName = val.ListName,
                    Datamart = myDatamart.DisplayName,
                    DatamartId = myDatamart.DatamartId,
                    Count = val.Count,
                    CreatedDate = val.CreatedDate,
                    StatusName = val.StatusName
                };

                displayModel.Add(myModel);
            }

            switch (sortType)
            {
                case "date":
                    displayModel.OrderBy(l => l.CreatedDate);
                    break;
                case "market":
                    displayModel.OrderBy(l => l.Datamart);
                    break;
                default:
                    break;
            }

            return View(displayModel);
        }

        [HttpPost]
        public async Task<ActionResult> MyLists(int id, string sortType = "")
        {
            var token = Session["token"].ToString();
            Session["sortType"] = sortType;
            var model = await caller.GetWithAuthorization<IEnumerable<ListViewModel>>(apiMethods.ListsBaseUrl(), string.Concat(apiMethods.GetListsByUserId, id), token);

            var displayModel = new List<MyListViewModel>();

            foreach (var val in model)
            {
                var myDatamart = await caller.GetWithAuthorization<DatamartViewModel>(apiMethods.FiltersBaseUrl(), string.Concat(apiMethods.GetAvailableDatamartById, val.Datamart), token);

                var myModel = new MyListViewModel
                {
                    SavedFilterId = val.SavedFilterId,
                    ListName = val.ListName,
                    Datamart = myDatamart.DisplayName,
                    DatamartId = myDatamart.DatamartId,
                    Count = val.Count,
                    CreatedDate = val.CreatedDate,
                    StatusName = val.StatusName
                };

                displayModel.Add(myModel);
            }

            switch (sortType)
            {
                case "date":
                    displayModel = displayModel.OrderBy(l => l.CreatedDate).ToList();
                    break;
                case "market":
                    displayModel = displayModel.OrderBy(l => l.Datamart).ToList();
                    break;
                default:
                    break;
            }

            return View(displayModel);
        }

        public async Task<ActionResult> DeleteList(int savedFilterId, bool option)
        {
            string ReturnURL = "/Logged/MyLists";
            var id = Session["userId"].ToString();
            var token = Session["token"].ToString();
            var userId = 0;
            int.TryParse(Session["userId"].ToString(), out userId);

            await caller.PostJsonWithAuthorization<int>(apiMethods.ListsBaseUrl(), apiMethods.DeleteListById, savedFilterId, token);

            if (option)
            {
                return Json(ReturnURL);
            }
            else
            {
                return RedirectToAction("MyLists");           
            }      
        }

        public async Task<ActionResult> ConsumerState(int id, int parent,int? groupId, int? filterId)
        {
            var token = Session["token"].ToString();
            Session["datamartId"] = id;

            var modelGroups = caller.GetWithAuthorizationn<IEnumerable<GroupViewModel>>(apiMethods.FiltersBaseUrl(), string.Concat(apiMethods.GetGroupsByDatamartId, id, "/", parent), token);
            if (groupId == null)
            {
                groupId = modelGroups.ToList()[0].GroupId;
            }

            var modelFilters = await caller.GetWithAuthorization<IEnumerable<FilterViewModel>>(apiMethods.FiltersBaseUrl(), string.Concat(apiMethods.GetFiltersByDaramartId, id, "/", parent), token);
           
                var filterValues = await caller.GetWithAuthorization<IEnumerable<FilterValuesViewModel>>(apiMethods.FiltersBaseUrl(), string.Concat(apiMethods.GetFilterValuesByFilterId, modelFilters.FirstOrDefault().FilterId), token);

            var myDatamart = await caller.GetWithAuthorization<DatamartViewModel>(apiMethods.FiltersBaseUrl(), string.Concat(apiMethods.GetAvailableDatamartById, id), token);

            var myModel = new ConsumerStateViewModel
            {
                Datamart = myDatamart.DisplayName,
                FilterModelObj = filterValues
            };

            Session["whichScreen"] = "ConsumerState";
            return View(myModel);
        }

        public async Task<ActionResult> ConsumerFilters(int id, int parent, int? groupId, int? filterId)
        {
            var token = Session["token"].ToString();
            int datamartId;
            if (Session["whichScreen"].ToString() == "SeeDetails")
            {
                datamartId = id;
                Session["datamartId"] = id;
            }
            else
            {
                datamartId = int.Parse(Session["datamartId"].ToString());
            }

            //New 
            var allFilters = await caller.GetWithAuthorization<IEnumerable<FilterViewModel>>(apiMethods.FiltersBaseUrl(), string.Concat(apiMethods.GetFiltersByDaramartId, id, "/", parent), token);
            var allFilterValues = await caller.GetWithAuthorization<IEnumerable<FilterValuesViewModel>>(apiMethods.FiltersBaseUrl(), string.Concat(apiMethods.GetFilterValuesByDatamartId, id, "/", parent), token);

            var modelGroups = caller.GetWithAuthorizationn<IEnumerable<GroupViewModel>>(apiMethods.FiltersBaseUrl(), string.Concat(apiMethods.GetGroupsByDatamartId, id, "/", parent), token);

            //New
            List<B2BFiltersViewModel> otherListModel = new List<B2BFiltersViewModel>();
  
            foreach (var group in modelGroups)
            {
                foreach (var filter in allFilters)
                {
                    if (group.GroupId == filter.GroupId)
                    {
                        foreach (var filterValue in allFilterValues)
                        {
                            if (filter.FilterId == filterValue.FilterId)
                            {
                                B2BFiltersViewModel other = new B2BFiltersViewModel();
                                
                                other.GroupId = group.GroupId;
                                other.FilterId = filter.FilterId;
                                other.FilterValueId = filterValue.FilterValueId;
                                other.GroupName = group.DisplayName;
                                other.FilterName = filter.DisplayName;
                                other.FilterValueName = filterValue.DisplayName;

                                otherListModel.Add(other);
                            }
                        }
                    }
                }
            }

            if (groupId == null)
            {
                groupId = modelGroups.ToList()[0].GroupId;
            }
            var modelFilters = caller.GetWithAuthorizationn<IEnumerable<FilterViewModel>>(apiMethods.FiltersBaseUrl(), string.Concat(apiMethods.GetFiltersByGroupId, groupId), token);
            if(filterId == null)
            {
                filterId = modelFilters.ToList()[0].FilterId;
            }
            var modelFilterValues = caller.GetWithAuthorizationn<IEnumerable<FilterValuesViewModel>>(apiMethods.FiltersBaseUrl(), string.Concat(apiMethods.GetFilterValuesByFilterId, filterId), token);

            var myDatamart = await caller.GetWithAuthorization<DatamartViewModel>(apiMethods.FiltersBaseUrl(), string.Concat(apiMethods.GetAvailableDatamartById, datamartId), token);

            var myModel = new ConsumerFiltersViewModel
            {
                Datamart = myDatamart.DisplayName,
                GroupModelObj = modelGroups,
                FilterModelObj = modelFilters,
                FilterValueModelObj = modelFilterValues
            };

            Session["parent"] = parent;
            Session["groupId"] = groupId;
            Session["filterId"] = filterId;
            Session["whichScreen"] = "ConsumerFilters";
            return View(myModel);
        }

        [HttpGet]
        public async Task<ActionResult> GetListInProgressItems(int userId, string viewName)
        {
            var datamartId = int.Parse(Session["datamartId"].ToString());
            var token = Session["token"].ToString();
            var whichScreen = Session["whichScreen"].ToString();

            var listInProgress = await caller.GetWithAuthorization<IEnumerable<ListInProgressItemViewModel>>(apiMethods.ListInProgressBaseUrl(), string.Concat(apiMethods.GetListInProgress, userId), token);
            
            var listInProgressGrouped = (from item in listInProgress
                         group item by item.GroupName into itemsByGroup
                         select new ListInProgressByGroupItemViewModel
                         {
                             GroupName = itemsByGroup.Key,
                             ListItemsGroupedByFilter = (from itemByGroup in itemsByGroup
                                                         group itemByGroup by itemByGroup.FilterName into itemsByFilter
                                                         select new ListInprogressByFilterItemViewModel
                                                         {
                                                             FilterName = itemsByFilter.Key,
                                                             ListItems = (from itemByFilter in itemsByFilter
                                                                          select new ListInProgressItemViewModel
                                                                          {     ListItemId = itemByFilter.ListItemId,
                                                                                GroupId = itemByFilter.GroupId,
                                                                                FilterId = itemByFilter.FilterId,
                                                                                FilterValueId = itemByFilter.FilterValueId,
                                                                                GroupName = itemByFilter.GroupName,
                                                                                FilterName = itemByFilter.FilterName,
                                                                                FilterValueName = itemByFilter.FilterValueName
                                                                          }).ToList()
                                                         }).ToList()
                         }).ToList();

            var myDatamart = await caller.GetWithAuthorization<DatamartViewModel>(apiMethods.FiltersBaseUrl(), string.Concat(apiMethods.GetAvailableDatamartById, datamartId), token);

            var model = new SeeFiltersViewModel
            {
                BackToScreen = whichScreen,
                Datamart = myDatamart.DisplayName,
                ListItemsGrouped = listInProgressGrouped
            };
            return View(viewName, model);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteListInProgressItem(int userId, int filterValueId)
        {
            var token = Session["token"].ToString(); 
            await caller.PostJsonWithAuthorization<int>(apiMethods.ListInProgressBaseUrl(), string.Concat(apiMethods.DeleteListInProgressItem, "?userId=", userId, "&filterValueId=", filterValueId), 0, token);
            return RedirectToAction("GetListInProgressItems", new { userId = userId, viewName = "SeeFilters"});
        }

        [HttpPost]
        public async Task<ActionResult> AddList(string listName)
        {
            var token = Session["token"].ToString();
            var userId = int.Parse(Session["userId"].ToString());
            var count = int.Parse(Session["countVal"].ToString());
            var datamartId = int.Parse(Session["datamartId"].ToString());
            var savedFilterDto = new SavedFilterDto
            {
                ListName = listName,
                UserId = userId,
                Count = count,
                Datamart = datamartId
            };
            await caller.PostJsonWithAuthorization<SavedFilterDto>(apiMethods.ListsBaseUrl(), apiMethods.AddList, savedFilterDto, token);
            return View("CompleteList");
        }

        [HttpGet]
        public async Task<ActionResult> SeeDetails(int savedFilterId)
        {
            var token = Session["token"].ToString();

            var seeDetailsList = await caller.GetWithAuthorization<IEnumerable<MyListDetailsViewModel>>(apiMethods.ListsBaseUrl(), string.Concat(apiMethods.GetMyListDetails, savedFilterId), token);
            var myList = await caller.GetWithAuthorization<SavedFilterViewModel>(apiMethods.ListsBaseUrl(), string.Concat(apiMethods.GetMyList, savedFilterId), token);

            var groupedList = (from item in seeDetailsList
                         group item by item.GroupName into itemsByGroup
                         select new MyListDetailsByGroupViewModel
                         {
                             GroupName = itemsByGroup.Key,
                             ListItemsByFilter = (from itemByGroup in itemsByGroup
                                                         group itemByGroup by itemByGroup.FilterName into itemsByFilter
                                                         select new MyListDetailsByFilterViewModel
                                                         {
                                                             FilterName = itemsByFilter.Key,
                                                             ListItems = (from itemByFilter in itemsByFilter
                                                                          select new MyListDetailsViewModel
                                                                          {
                                                                              SavedFilterId = itemByFilter.SavedFilterId,
                                                                              SavedId = itemByFilter.SavedId,
                                                                              GroupId = itemByFilter.GroupId,
                                                                              FilterId = itemByFilter.FilterId,
                                                                              FilterValueId = itemByFilter.FilterValueId,
                                                                              GroupName = itemByFilter.GroupName,
                                                                              FilterName = itemByFilter.FilterName,
                                                                              FilterValueName = itemByFilter.FilterValueName,
                                                                              Count = itemByFilter.Count
                                                                          }).ToList()
                                                         }).ToList()
                         }).ToList();

            var model = new SeeDetailsViewModel
            {
                Datamart = myList.Datamart,
                ListItemsByGroup = groupedList,
                ListName = myList.ListName,
                EntireCount = myList.Count,
                CreatedDate = myList.CreatedDate,
                SavedFilterId = myList.SavedFilterId
            };

            Session["whichScreen"] = "SeeDetails";
            return View(model);
        }

        public async Task<ActionResult> CompleteList()
        {
            return View();
        }

        #region Contact & About

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        #endregion

        public async Task<ActionResult> Admin(int? page)
        {
            var token = Session["token"].ToString();
            var myPage = page ?? 1;

            var model = await caller.GetWithAuthorization<IEnumerable<DisplayUsersViewModel>>(apiMethods.UserBaseUrl(), apiMethods.GetUsers, token);
            
            return View(model.ToList().ToPagedList(myPage, 5));
        }

        [HttpPost]
        public async Task<ActionResult> AdminSaveChanges(IEnumerable<UsersToEnable> list)
        {
            var token = Session["token"].ToString();

            await caller.PostJsonWithAuthorization<IEnumerable<UsersToEnable>>(apiMethods.UserBaseUrl(), apiMethods.EnableUsers, list, token);

            return View(RedirectToAction("Admin"));
        }
       
        public async Task<ActionResult> ResetPassword(int userId)
        {
            var token = Session["token"].ToString();

            string password = Membership.GeneratePassword(12, 1);

            var model = await caller.GetWithAuthorization<UserDto>(apiMethods.UserBaseUrl(), string.Concat(apiMethods.GetUserById, userId), token);
            model.Password = Sha256Hash(password);
            SendEmail(model.Email, password);

            await caller.PostJsonWithAuthorization<UserDto>(apiMethods.UserBaseUrl(), apiMethods.ResetPassword, model, token);

            return RedirectToAction("Admin");
        }

        public void SendEmail(string email, string password)
        {
            MailMessage mail = new MailMessage("aorbai@riasolutionsgroup.com", email, "[DataMarket] Confirmed Password Change!", "Your new password is:" + Environment.NewLine + password);
            mail.BodyEncoding = UTF8Encoding.UTF8;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "smtp.gmail.com";
            client.Credentials = new NetworkCredential("aorbai@riasolutionsgroup.com", "remover12");

            client.Send(mail);
        }

        private static String Sha256Hash(String value)
        {
            using (SHA256 hash = SHA256Managed.Create())
            {
                return String.Join("", hash
                  .ComputeHash(Encoding.UTF8.GetBytes(value))
                  .Select(item => item.ToString("x2")));
            }
        }
    }
}