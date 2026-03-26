using Microsoft.AspNetCore.Mvc;
using Shoping_Karo.Models;
using Shoping_Karo.Services.Interface;

namespace Shoping_Karo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlist _wishlistService;

        public WishlistController(IWishlist wishlistService)
        {
            _wishlistService = wishlistService;
        }

        // GET: api/Wishlist/user/5
        [HttpGet("user/{userId}")]
        public IActionResult GetWishlistByUserId(int userId)
        {
            var wishlistItems = _wishlistService.getWishlistByUserId(userId);

            return Ok(wishlistItems);
        }

        // POST: api/Wishlist
        [HttpPost]
        public IActionResult AddWishlist([FromBody] wishlist wishlistItem)
        {
            if (wishlistItem == null)
                return BadRequest(new { message = "Wishlist item cannot be null." });

            _wishlistService.addWishlist(wishlistItem);
            return Ok(new { message = "Wishlist item added successfully." });
        }

        // DELETE: api/Wishlist/5
        [HttpDelete("{id}")]
        public IActionResult DeleteWishlist(int id)
        {
            _wishlistService.deleteWishlistid(id);
            return Ok(new { message = $"Wishlist item with Id {id} deleted successfully." });
        }
    }
}