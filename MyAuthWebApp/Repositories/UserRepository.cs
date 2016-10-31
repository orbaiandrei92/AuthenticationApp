using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyAuthWebApp.Entities;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace MyAuthWebApp
{
    public class UserRepository : IDisposable
    {
        private DataMarketEntities _userContext;


        public UserRepository()
        {
            _userContext = new DataMarketEntities();
        }

        public async Task<User> FindUser(string userName, string password)
        {
            var user = _userContext.Users
                .Where(u => u.UserName.Equals(userName))
                .Where(u => u.Password.Equals(password)).FirstOrDefault();

            return user;
        }

        public void Dispose()
        {
            _userContext.Dispose();
        }
    }
}