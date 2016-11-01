using DataMarket.DTOs;
using DataMarket.Infrastructure;
using DataMarket.Web.Models;
using DataMarket.Web.Models.ViewModels;
using LoginLibrary;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using System.Web.Mvc;
using static DataMarket.Infrastructure.ApiCaller;

namespace DataMarket.Web.Controllers
{
  
    public class HomeController : Controller
    {
        
        private readonly ILogger logger;
        private readonly IApiCaller caller;
        private IApiMethods apiMethods;

        public HomeController(ILogger logger, IApiCaller caller, IApiMethods apiMethods)
        {
            this.logger = logger;
            this.caller = caller;
            this.apiMethods = apiMethods;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Index", model);
                }
                
                var userAccount = new UserAccount();
                var hashPassword = Sha256Hash(model.Password);
                var token = await userAccount.Login(model.UserName, hashPassword);
                var user = await caller.GetWithAuthorization<UserDto>(apiMethods.UserBaseUrl(), string.Concat(apiMethods.GetUserByUserName, model.UserName), token);

                if (user.IsEnabled == false)
                {
                    ViewBag.Error = "Your account is not Enabled. Please contact the admin!";
                    return View("Index", model);
                }

                Session["token"] = token;
                Session["userName"] = string.Format("{0} {1}", user.FirstName, user.LastName);
                Session["userId"] = user.UserId;
                Session["isAdmin"] = user.IsAdmin;
                ViewBag.DbError = "";
                return RedirectToAction("Create", "Logged");
            }
            catch(Exception)
            {
                ViewBag.DbError = "Username or password incorrect!";
                return View("Index", model);
            }

        }
        public ActionResult Logout()
        {
            Session.Clear();
            return View("Index");
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

        #region SHA256 Method
        private static String Sha256Hash(String value)
        {
            using (SHA256 hash = SHA256Managed.Create())
            {
                return String.Join("", hash
                  .ComputeHash(Encoding.UTF8.GetBytes(value))
                  .Select(item => item.ToString("x2")));
            }
        }
        #endregion
    }
}