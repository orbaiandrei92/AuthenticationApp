using DataMarket.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMarket.Business
{
    public interface IUserManager: IDisposable
    {
        Task<UserDto> GetById(int id);
        Task ChangePassword(int id, string password);
        Task EnableUsers(IEnumerable<UsersToEnable> myUsers);
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<UserDto> GetUserByUserName(string userName);
    }
}
