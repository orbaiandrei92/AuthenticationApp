using System.Threading.Tasks;

namespace DataMarket.Infrastructure
{
    public interface IApiCaller
    {
        Task<TReturn> Get<TReturn>(string baseUrl, string method);
        Task<TReturn> GetWithAuthorization<TReturn>(string baseUrl, string method, string token);
        T GetWithAuthorizationn<T>(string baseUrl, string method, string token);


        Task PostJson<TRequest>(string baseUrl, string method, TRequest request) where TRequest : class;
        Task PostJsonWithAuthorization<TRequest>(string baseUrl, string method, TRequest request, string token);
        Task<TReturn> PostJson<TRequest, TReturn>(string baseUrl, string method, TRequest request)
            where TRequest : class
            where TReturn : class;




        Task PostWithAuthorization<TRequest>(string baseUrl, string method, int userId, TRequest request, string token = "")
                where TRequest : class;
                
        Task<TReturn> PostWithAuthorization<TRequest , TReturn>(string baseUrl, string method,int userId,  TRequest request, string token = "")
               where TRequest : class
               where TReturn : class;

        Task<T> PostWithAuthorization2<TRequest, T>(string baseUrl, string method, int userId, TRequest request, string token = "")
       where TRequest : class;
      
    }
}