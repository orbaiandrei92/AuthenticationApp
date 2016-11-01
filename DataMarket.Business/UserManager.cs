using DataMarket.Data;
using DataMarket.Infrastructure;
using System.Threading.Tasks;
using DataMarket.DTOs;
using System.Collections.Generic;

namespace DataMarket.Business
{
    public class UserManager: IUserManager
    {
        private readonly ILogger _logger;
        private readonly IMapperResolver _mapper;
        private readonly IUserRepository _repository;

        public UserManager(ILogger logger, IMapperResolver mapperResolver, IUserRepository userRepository)
        {
            this._logger = logger;
            this._mapper = mapperResolver;
            this._repository = userRepository;
        }
        public void Dispose()
        {
            if (_repository != null)
                _repository.Dispose();
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = await _repository.GetAllUsers();
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);
            return usersDto;
        }

        public async Task EnableUsers(IEnumerable<UsersToEnable> myUsers)
        {
            var users = _mapper.Map<IEnumerable<User>>(myUsers);
            await _repository.EnableUsers(users);
        }

        public async Task<UserDto> GetUserByUserName(string userName)
        {
            var user = await _repository.GetUserByUserName(userName);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<UserDto> GetById(int id)
        {
            var user = await _repository.GetById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task ChangePassword(int id, string password)
        {
            await _repository.ChangePassword(id, password);
        }
    }
}
