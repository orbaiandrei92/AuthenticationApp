using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MyAuthWebApp
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {

        public AccountController()
        {
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }   
    }
}