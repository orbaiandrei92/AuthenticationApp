using DataMarket.Business;
using DataMarket.DTOs;
using DataMarket.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace DataMarketResources.Api.Controllers
{
    [RoutePrefix("api/users")]
    [Authorize]
    public class UserController : ApiController
    {
        private readonly ILogger _logger;
        private readonly IUserManager _userManager;

        public UserController(ILogger logger, IUserManager userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [Route("userId/{id}")]
        [HttpGet]
        public async Task<UserDto> GetUser(int id)
        {
            _logger.Log(LoggingLevel.Info, "GetUser started");
            var user = await _userManager.GetById(id);
            return user;
        }

        [Route("users")]
        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            _logger.Log(LoggingLevel.Info, "GetUsers started");
            var users = await _userManager.GetAllUsers();
            return users;
        }

        [Route("user/{userName}")]
        [HttpGet]
        public async Task<UserDto> GetUserByUserName(string userName)
        {
            _logger.Log(LoggingLevel.Info, "GetUserByUserName started");
            var user = await _userManager.GetUserByUserName(userName);
            return user;
        }

        [Route("enableUsers")]
        [HttpPost]
        public async Task EnableUsers([FromBody]IEnumerable<UsersToEnable> myUsers)
        {
            try
            {
                _logger.Log(LoggingLevel.Info, "EnableUsers started");
                await _userManager.EnableUsers(myUsers);
            }
            catch (Exception ex)
            {
                _logger.Log(LoggingLevel.Error, "There was an error : ", ex);
            }
        }

        [Route("resetPassword")]
        [HttpPost]
        public async Task ResetPassword([FromBody] UserDto user)
        {
            try
            {
                _logger.Log(LoggingLevel.Info, "ResetPassword started");
                await _userManager.ChangePassword(user.UserId, user.Password);
            }
            catch (Exception ex)
            {
                _logger.Log(LoggingLevel.Error, "There was an error : ", ex);
            }
        }
    }
}
