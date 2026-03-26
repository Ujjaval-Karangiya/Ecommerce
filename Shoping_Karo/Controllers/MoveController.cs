using Microsoft.AspNetCore.Mvc;
using Shoping_Karo.Services.Interface;

namespace Shoping_Karo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoveController : ControllerBase
    {
        private readonly IMoveService _moveService;

        public MoveController(IMoveService moveService)
        {
            _moveService = moveService;
        }

        // POST: api/Move/wishlist-to-cart/2
        [HttpPost("wishlist-to-cart/{userId}")]
        public IActionResult MoveWishlistToCart(string userId)
        {
            _moveService.MoveWishlistToCart(userId);
            return Ok("Wishlist moved to cart");
        }

        // POST: api/Move/single-wishlist-to-cart/4
        [HttpPost("single-wishlist-to-cart/{id}")]
        public IActionResult MoveSingleWishlistToCart(int id)
        {
            _moveService.MoveSingleWishlistToCart(id);
            return Ok("Single wishlist item moved to cart");
        }

        // POST: api/Move/cart-to-order/1
        [HttpPost("cart-to-order/{userId}")]
        public IActionResult MoveCartToOrder(string userId)
        {
            _moveService.MoveCartToOrder(userId);
            return Ok("Cart moved to order");
        }

        // POST: api/Move/single-cart-to-order/1/1
        [HttpPost("single-cart-to-order/{userId}/{productId}")]
        public IActionResult MoveSingleCartToOrder(string userId, int productId)
        {
            _moveService.MoveSingleCartToOrder(userId, productId);
            return Ok("Single cart item moved to order");
        }
    }
}