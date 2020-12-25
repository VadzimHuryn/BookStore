using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using BookStore.Services;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Http;

namespace BookStore.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        UserService _userService;

        public UserController(IConfiguration configuration)
        {
            _userService = new UserService(configuration);
        }

        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            return _userService.GetAllUsers();
        }

        [HttpGet]
        [Route("Roles")]
        public IEnumerable<UserRole> GetUserRoles()
        {
            return _userService.GetUserRoles();
        }

        [HttpPost]
        public int Add([FromBody] User user)
        {
            var result = _userService.Add(user);
            return result;
        }

        [HttpPost]
        [Route("LogIn")]
        public User CheckUser([FromBody] UserLogin userLogin)
        {
            return _userService.GetUserByUserLogin(userLogin);
        }

    }
}
