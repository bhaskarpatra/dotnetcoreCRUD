using Microsoft.AspNetCore.Mvc;
using WebApplicationDBManager.DBContext;

namespace dotNetWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;
        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetOrders")]
        public IActionResult GetProducts(string orderId)
        {
            return Ok(_context.Order.FirstOrDefault(x => x.Id == orderId));
        }
    }
}
