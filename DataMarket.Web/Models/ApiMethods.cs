using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataMarket.Web.Models
{
    public class ApiMethods: IApiMethods
    {
        private const string FiltersController = "api/filters";
        private const string ListsController = "api/lists";
        private const string UserController = "api/users";
        private const string ListInProgressController = "api/listInProgress";

        private string apiBaseUrl;
        public ApiMethods(string apiBaseUrl)
        {
            this.apiBaseUrl = apiBaseUrl;
        }

        public string FiltersBaseUrl()
        {
            return string.Format("{0}{1}/", this.apiBaseUrl, FiltersController);
        }

        public string ListsBaseUrl()
        {
            return string.Format("{0}{1}/", this.apiBaseUrl, ListsController);
        }

        public string UserBaseUrl()
        {
            return string.Format("{0}{1}/", this.apiBaseUrl, UserController);
        }

        public string ListInProgressBaseUrl()
        {
            return string.Format("{0}{1}/", this.apiBaseUrl, ListInProgressController);
        }

        public string GetUsers
        {
            get
            {
                return "users";
            }
        }

        public string GetUserById
        {
            get
            {
                return "userId/";
            }
        }

        public string EnableUsers
        {
            get
            {
                return "enableUsers";
            }
        }

        public string ResetPassword
        {
            get
            {
                return "resetPassword";
            }
        }

        public string DeleteListById
        {
            get
            {
                return "deleteList";
            }
        }

        public string GetMyListDetails
        {
            get
            {
                return "myListDetails/";
            }
        }

        public string GetMyList
        {
            get
            {
                return "myList/";
            }
        }

        string IApiMethods.GetAvailableDatamarts
        {
            get
            {
                return "datamarts";                      
            }
        }

        string IApiMethods.GetAvailableDatamartById
        {
            get
            {
                return "datamarts/";
            }
        }

        string IApiMethods.GetFiltersByDaramartId
        {
            get
            {
                return "bydatamart/";
            }
        }

        public string Token { get; set; }

       string IApiMethods.GetGroupsByDatamartId
        {
            get
            {
                return "groups/";
            }
        }

        public string GetListsByUserId
        {
            get
            {
                return "myLists/";
            }
        }

        public string GetFilterValuesByDatamartId
        {
            get
            {
                return "filterValues/";
            }
        }

        public string GetUserByUserName
        {
            get
            {
                return "user/";
            }
        }

        string IApiMethods.GetFiltersByGroupId
        {
            get
            {
                return "filters/";
            }
        }

        public string GetFilterValuesByFilterId
        {
            get
            {
                return "filterValues/";
            }
        }
        string IApiMethods.getCountAndSaveDataMarketListInProgresController
        {
            get
            {
                return "getCountAndSaveDataMarketListInProgres/";
            }
        }

        public string GetListInProgress
        {
            get
            {
                return "userList/";
            }
        }

        public string DeleteListInProgressItem
        {
            get
            {
                return "deleteItem";
            }
        }

        public string AddList
        {
            get
            {
                return "addList/";
            }
        }
    }
}