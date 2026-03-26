using Microsoft.Data.SqlClient;
using Shoping_Karo.Data;
using Shoping_Karo.Services.Interface;
using System.Data;

namespace Shoping_Karo.Services.Implementation
{
    public class MoveService : IMoveService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<MoveService> _logger;

        public MoveService(AppDbContext context, ILogger<MoveService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void MoveWishlistToCart(string userId)
        {
            try
            {
                using (SqlConnection con = _context.connection())
                using (SqlCommand cmd = new SqlCommand("sp_MoveWishlistToCart", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@user_id", userId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                _logger.LogInformation("✅ Wishlist → Cart moved");
            }
            catch (Exception ex)
            {
                _logger.LogError("❌ Error: " + ex.Message);
            }
        }

        public void MoveSingleWishlistToCart(int id)
        {
            try
            {
                using (SqlConnection con = _context.connection())
                using (SqlCommand cmd = new SqlCommand("sp_MoveSingleWishlistToCart", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                _logger.LogInformation("✅ Single Wishlist → Cart moved");
            }
            catch (Exception ex)
            {
                _logger.LogError("❌ Error: " + ex.Message);
            }
        }

        public void MoveCartToOrder(string userId)
        {
            try
            {
                using (SqlConnection con = _context.connection())
                using (SqlCommand cmd = new SqlCommand("sp_MoveCartToOrder", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@user_id", userId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                _logger.LogInformation("✅ Cart → Order moved");
            }
            catch (Exception ex)
            {
                _logger.LogError("❌ Error: " + ex.Message);
            }
        }

        public void MoveSingleCartToOrder(string userId, int productId)
        {
            try
            {
                using (SqlConnection con = _context.connection())
                using (SqlCommand cmd = new SqlCommand("sp_MoveSingleCartToOrder", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    cmd.Parameters.AddWithValue("@product_id", productId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                _logger.LogInformation("✅ Single Cart → Order moved");
            }
            catch (Exception ex)
            {
                _logger.LogError("❌ Error: " + ex.Message);
            }
        }
    }
}