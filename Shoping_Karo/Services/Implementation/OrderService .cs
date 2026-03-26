using Microsoft.Data.SqlClient;
using Shoping_Karo.Data;
using Shoping_Karo.Models;
using Shoping_Karo.Services.Interface;
using System.Data;

namespace Shoping_Karo.Services.Implementation
{
    public class OrderService : IOrder
    {
        private readonly AppDbContext _context;
        private readonly ILogger<OrderService> _logger;

        public OrderService(AppDbContext context, ILogger<OrderService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddOrder(orders order)
        {
            try
            {
                using (SqlConnection con = _context.connection())
                using (SqlCommand sq = new SqlCommand("sp_InsertOrder", con))
                {
                    sq.CommandType = CommandType.StoredProcedure;

                    sq.Parameters.AddWithValue("@user_id", order.user_id);
                    sq.Parameters.AddWithValue("@product_id", order.product_id);
                    sq.Parameters.AddWithValue("@title", order.title);
                    sq.Parameters.AddWithValue("@price", order.price);
                    sq.Parameters.AddWithValue("@qty", order.qty);
                    sq.Parameters.AddWithValue("@image", order.image);

                    con.Open();
                    sq.ExecuteNonQuery();
                }

                _logger.LogInformation("✅ Order inserted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("❌ Error: " + ex.Message);
            }
        }

        public List<orders> GetOrdersByUser(string userId)
        {
            List<orders> orders = new List<orders>();

            try
            {
                using (SqlConnection con = _context.connection())
                using (SqlCommand sq = new SqlCommand("sp_GetOrdersByUser", con))
                {
                    sq.CommandType = CommandType.StoredProcedure;
                    sq.Parameters.AddWithValue("@user_id", userId);

                    con.Open();

                    using (SqlDataReader reader = sq.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orders o = new orders()
                            {
                                id = Convert.ToInt32(reader["id"]),
                                user_id = Convert.ToInt32(reader["user_id"]),
                                product_id = Convert.ToInt32(reader["product_id"]),
                                title = reader["title"].ToString(),
                                price = Convert.ToDecimal(reader["price"]),
                                qty = Convert.ToInt32(reader["qty"]),
                                image = reader["image"].ToString()
                            };
                            orders.Add(o);
                        }
                    }
                }

                _logger.LogInformation("✅ Orders fetched successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("❌ Error: " + ex.Message);
            }

            return orders;
        }
    }
}