using DataMarket.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataMarket.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataMarketDbContext _dataMarketDbContext;
        private readonly ILogger _logger;

        public UserRepository(ILogger logger)
        {
            _dataMarketDbContext = new DataMarketDbContext();
            _logger = logger;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = _dataMarketDbContext.Users.ToList();
            return users;
        }

        public async Task<User> GetById(int id)
        {
            var user = _dataMarketDbContext.Users.Where(u => u.UserId == id).FirstOrDefault();
            return user;
        }

        public async Task EnableUsers(IEnumerable<User> users)
        {
            using (var sequenceEnum = users.GetEnumerator())
            {
                while (sequenceEnum.MoveNext())
                {
                    var user = GetById(sequenceEnum.Current.UserId).Result;
                    user.IsEnabled = sequenceEnum.Current.IsEnabled;
                    _dataMarketDbContext.SaveChanges();
                }
            }
        }

        public async Task ChangePassword(int id, string password)
        {
            var user = _dataMarketDbContext.Users.Where(u => u.UserId == id).FirstOrDefault();

            user.Password = password;
            _dataMarketDbContext.SaveChanges();
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            var user = _dataMarketDbContext.Users.Where(u => u.UserName == userName).FirstOrDefault();
            return user;
        }

        public void Dispose()
        {
            if (_dataMarketDbContext != null)
            {
                _dataMarketDbContext.Dispose();
            }
        }
    }
}
