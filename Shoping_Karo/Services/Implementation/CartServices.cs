using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Shoping_Karo.Data;
using Shoping_Karo.Models;
using Shoping_Karo.Services.Interface;
using System.Data;

namespace Shoping_Karo.Services.Implementation
{
    public class CartServices : ICart
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CartServices> _logger;
        public CartServices(AppDbContext context, ILogger<CartServices> logger)
        {
            _context = context;
            _logger = logger;
        }
        public void addToCart(cart cart)
        {
            try
            {
                var con = _context.connection();
                using (SqlCommand sq = new SqlCommand("sp_AddToCart", con))
                {
                    sq.CommandType = CommandType.StoredProcedure;
                    sq.Parameters.AddWithValue("@user_id", cart.user_id);
                    sq.Parameters.AddWithValue("@product_id", cart.product_id);
                    sq.Parameters.AddWithValue("@title", cart.title);
                    sq.Parameters.AddWithValue("@price", cart.price);
                    sq.Parameters.AddWithValue("@qty", cart.qty);
                    sq.Parameters.AddWithValue("@image", cart.image);
                    con.Open();
                    sq.ExecuteNonQuery();
                }
                _logger.LogInformation("add to cart successfully🎉");
            } catch (Exception ex)
            {
                _logger.LogError("❌ you Have Exception :" + ex);
            }
        }


        public void DecreaseCartIdQty(int id)
        {
            try
            {
                var con = _context.connection();
                using (SqlCommand sq = new SqlCommand("sp_DecreaseCartQty", con))
                {
                    sq.CommandType = CommandType.StoredProcedure;
                    sq.Parameters.AddWithValue("@user_id", id);
                    con.Open();
                    sq.ExecuteNonQuery();
                }
                _logger.LogInformation("Cart qty decrease Successfully🎉");
            }
            catch (Exception ex)
            {
                _logger.LogError("❌ You Have Exception :" + ex);
            }
        }

        public void DeleteCartId(int id)
        {
            try
            {
                var con = _context.connection();
                using (SqlCommand sq = new SqlCommand("sp_DeleteCart", con))
                {
                    sq.CommandType = CommandType.StoredProcedure;
                    sq.Parameters.AddWithValue("@user_id", id);
                    con.Open();
                    sq.ExecuteNonQuery();
                }
                _logger.LogInformation("Cart Delete Successfully🎉");
            }
            catch (Exception ex)
            {
                _logger.LogError("❌ You Have Exception :" + ex);
            }
        }


        public List<cart> getCartByUserId(int id)
        {

            List<cart> carts = new List<cart>();
            var con = _context.connection();
            using (SqlCommand qr = new SqlCommand("sp_GetCartByUser", con))
            {
                qr.CommandType = CommandType.StoredProcedure;
                qr.Parameters.AddWithValue("@user_id", id);
                con.Open();
                using (SqlDataReader reader = qr.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cart p = new cart()
                        {
                            user_id = Convert.ToInt32(reader["user_id"]),
                            product_id = Convert.ToInt32(reader["product_id"]),
                            title = reader["Title"].ToString(),
                            price = Convert.ToDecimal(reader["Price"]),
                            image = reader["Image"].ToString(),
                            qty = Convert.ToInt32(reader["qty"])
                        };
                        carts.Add(p);
                    }
                }
            }
            return carts;
        }
    } 
}
