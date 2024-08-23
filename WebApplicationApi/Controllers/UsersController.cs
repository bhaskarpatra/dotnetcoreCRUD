using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationDBManager.Interface;
using WebApplicationDBManager.Models;
using WebApplicationDBManager.ViewModels;

namespace dotNetWebApplication.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(UserViewModel userVM)
        {
            var response = _userRepo.Login(userVM);
            if (response.GetType() == typeof(String))
            {
                return Ok(new { userVM, response });
            }
            return BadRequest(response);            
        }

        [Authorize]
        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUsers()
        {
            var usersList = _userRepo.GetUsers();
            if(usersList.FirstOrDefault().GetType() == typeof(UserViewModel))
            {
                return Ok(usersList);
            }
            else
            {
                return BadRequest(usersList.FirstOrDefault());
            }
        }

        [HttpPost]
        [Route("AddUser")]
        public IActionResult AddUser(UserViewModel userVM)
        {
            var user = _userRepo.AddUser(userVM);
            if(user.GetType() == typeof(User))
            {
                return Ok(user);
            }
            return BadRequest(user);
        }
    }
}
