using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationDBManager.DBContext;
using WebApplicationDBManager.ViewModels;

namespace dotNetWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsersController(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(200);
        }

        [HttpPost]
        public IActionResult AddUsers()
        {
            return Ok(200);
        }
    }
}
