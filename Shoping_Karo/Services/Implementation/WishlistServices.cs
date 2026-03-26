using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shoping_Karo.Data;
using Shoping_Karo.Models;
using Shoping_Karo.Services.Interface;
using System.Data;

namespace Shoping_Karo.Services.Implementation
{
    public class WishlistServices : IWishlist
    {
        private readonly AppDbContext _context;
        private readonly ILogger<WishlistServices> _logger;
        public WishlistServices(AppDbContext context, ILogger<WishlistServices> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void addWishlist(wishlist wishlist)
        {
            try
            {
                var con = _context.connection();
                using (SqlCommand sq = new SqlCommand("sp_InsertWishlist", con))
                {
                    sq.CommandType = CommandType.StoredProcedure;
                    sq.Parameters.AddWithValue("@user_id", wishlist.user_id);
                    sq.Parameters.AddWithValue("@product_id", wishlist.product_id);
                    sq.Parameters.AddWithValue("@title", wishlist.title);
                    sq.Parameters.AddWithValue("@price", wishlist.price);
                    sq.Parameters.AddWithValue("@image", wishlist.image);
                    con.Open();
                    sq.ExecuteNonQuery();
                }
                _logger.LogInformation(" successfully🎉");
            }
            catch (Exception ex)
            {
                _logger.LogError("❌ you Have Exception :" + ex);
            }
        }

       
        public void deleteWishlistid(int id)
        {
            try
            {
                var con = _context.connection();
                using (SqlCommand sq = new SqlCommand("sp_DeleteWishlist", con))
                {
                    sq.CommandType = CommandType.StoredProcedure;
                    sq.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    sq.ExecuteNonQuery();
                }
                _logger.LogInformation(" successfully🎉");
            }
            catch (Exception ex)
            {
                _logger.LogError("❌ you Have Exception :" + ex);
            }
        }

        public List<wishlist> getWishlistByUserId(int id)
        {
            List<wishlist> wishlists = new List<wishlist>();

            try
            {
                using (SqlConnection con = _context.connection())
                using (SqlCommand sq = new SqlCommand("sp_GetWishlistByUser", con)) // rename SP also
                {
                    sq.CommandType = CommandType.StoredProcedure;
                    sq.Parameters.AddWithValue("@user_id", id);

                    con.Open();

                    using (SqlDataReader reader = sq.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            wishlist w = new wishlist()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                user_id = Convert.ToInt32(reader["user_id"]),
                                product_id = Convert.ToInt32(reader["product_id"]),
                                title = reader["title"].ToString(),
                                price = Convert.ToDecimal(reader["price"]),
                                image = reader["image"].ToString()
                            };

                            wishlists.Add(w);
                        }
                    }
                }

                _logger.LogInformation("✅ Wishlist fetched successfully");
            }
            catch (SqlException ex)
            {
                _logger.LogError($"❌ SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ General Error: {ex.Message}");
            }
            Console.WriteLine(wishlists);
            return wishlists;
        }
    }
}
