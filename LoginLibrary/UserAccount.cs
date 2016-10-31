using LoginLibrary.Helpers;
using System.Threading.Tasks;
using System.Configuration;

namespace LoginLibrary
{
    public class UserAccount
    {
        private ApiCaller _apiCaller = new ApiCaller();
        private static string AuthenticationBaseUrl = "AuthenticationBaseUrl";

        public UserAccount()
        {
            _apiCaller = new ApiCaller();
        }
        public async Task<string> Login(string userName, string password)
        {
            string baseUrl = ConfigurationManager.AppSettings[AuthenticationBaseUrl];
            var token = await _apiCaller.PostJsonToken<string>(baseUrl, userName, password);
            return token;
        }

    }
}
