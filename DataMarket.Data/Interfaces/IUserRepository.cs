using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMarket.Data
{
    public interface IUserRepository: IDisposable
    {
        Task ChangePassword(int id, string password);
        Task<User> GetById(int id);
        Task EnableUsers(IEnumerable<User> users);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserByUserName(string userName);
    }
}
