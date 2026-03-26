using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoping_Karo.Models;
using Shoping_Karo.Services.Interface;

namespace Shoping_Karo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICart _cart;
        public CartController(ICart cart)
        {
            _cart = cart;
        }

        [HttpPost("AddCart")]
        public async Task<IActionResult> addToCart([FromBody]cart cart)
        {
            _cart.addToCart(cart);
            return Ok(new
            {
                cart = cart,
                message = "cart added successfully 🎉"
            });
        }

        [HttpDelete("DeleteCartID/{id}")]
        public IActionResult deleteCart(int id)
        {
            _cart.DeleteCartId(id);
            return Ok(new
            {
                message = "cart deleted successfully 🎉"
            });
        }

        [HttpGet("Userid/{id}")]
        public IActionResult getCart(int id)
        {
            var data = _cart.getCartByUserId(id);
            return Ok(data);
        }
        [HttpPut("decreaseQtyCartID/{id}")]
        public IActionResult qtyDecrease(int id)
        {
            _cart.DecreaseCartIdQty(id);
            return Ok(new
            {
                message = "cart decrease successfully 🎉"
            });
        }

    }
}
