namespace DataMarket.Web.Models
{
    public interface IApiMethods
    {
        string FiltersBaseUrl();

        string ListsBaseUrl();

        string UserBaseUrl();

        string ListInProgressBaseUrl();

        string GetAvailableDatamarts { get; }

        string GetAvailableDatamartById { get; }

        string GetFiltersByDaramartId { get; }

        string GetFilterValuesByDatamartId { get; }

        string GetMyList { get; }

        // string Token { get; }
        string GetUsers { get; }

        string GetUserById { get; }

        string EnableUsers { get; }

        string ResetPassword { get; }

        string GetGroupsByDatamartId { get; }

        string GetListsByUserId { get; }

        string AddList { get; }

        string GetUserByUserName { get; }

        string GetFiltersByGroupId { get; }

        string getCountAndSaveDataMarketListInProgresController { get; }

        string DeleteListById { get; }

        string GetMyListDetails { get; }

        string GetFilterValuesByFilterId { get; }

        string GetListInProgress { get;}

        string DeleteListInProgressItem { get; }
    }
}
