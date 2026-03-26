using Microsoft.AspNetCore.Mvc;
using Shoping_Karo.Models;
using Shoping_Karo.Services.Interface;

namespace Shoping_Karo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrder _orderService;

        public OrderController(IOrder orderService)
        {
            _orderService = orderService;
        }

        // POST: api/Order
        [HttpPost]
        public IActionResult AddOrder([FromBody] orders order)
        {
            if (order == null)
                return BadRequest("Invalid order data");

            _orderService.AddOrder(order);
            return Ok(new { message = "Order placed successfully" });
        }

        // GET: api/Order/user/4
        [HttpGet("user/{userId}")]
        public IActionResult GetOrdersByUser(string userId)
        {
            var orders = _orderService.GetOrdersByUser(userId);

            if (orders == null || !orders.Any())
                return NotFound("No orders found");

            return Ok(orders);
        }
    }
}