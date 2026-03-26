using Microsoft.Data.SqlClient;
using Shoping_Karo.Data;
using Shoping_Karo.Models;
using Shoping_Karo.Services.Interface;
using System.Data;

namespace Shoping_Karo.Services.Implementation
{
    public class ProductServices : IProduct
    {
        private readonly AppDbContext _context;
        public ProductServices(AppDbContext context)
        {
            _context = context;
        }
        public void Addproduct(product product)
        {
            var con = _context.connection();
            using (SqlCommand qr = new SqlCommand("sp_InsertProduct", con))
            {
                qr.CommandType = CommandType.StoredProcedure;
                qr.Parameters.AddWithValue("@Title", product.Title);
                qr.Parameters.AddWithValue("@Price", product.Price);
                qr.Parameters.AddWithValue("@Description", product.Description);
                qr.Parameters.AddWithValue("@Category", product.Category);
                qr.Parameters.AddWithValue("@Image", product.Image);
                qr.Parameters.AddWithValue("@Rate", product.Rate);
                qr.Parameters.AddWithValue("@Count", product.Count);
                con.Open();
                qr.ExecuteNonQuery();
            }
        }

        public void Deleteproduct(int id)
        {
            var con = _context.connection();
            using (SqlCommand qr = new SqlCommand("sp_DeleteProduct", con))
            {

                qr.CommandType = CommandType.StoredProcedure;
                qr.Parameters.AddWithValue("@Id",id);
                con.Open();
                qr.ExecuteNonQuery();
            }
        }

        public List<product> getProductById(int id)
        {
            List<product> products = new List<product>();
            var con = _context.connection();
            using (SqlCommand qr = new SqlCommand("sp_GetProductById", con))
            {
                qr.CommandType = CommandType.StoredProcedure;
                qr.Parameters.AddWithValue("@Id", id);
                con.Open();
                using (SqlDataReader reader = qr.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        product p = new product()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Title = reader["Title"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            Description = reader["Description"].ToString(),
                            Category = reader["Category"].ToString(),
                            Image = reader["Image"].ToString(),
                            Rate = Convert.ToDecimal(reader["Rate"]),
                            Count = Convert.ToInt32(reader["Count"])
                        };

                        products.Add(p);
                    }
                }
            }
                return products;
        }

        public List<product> showProduct()
        {
            List<product> products = new List<product>();
            var con = _context.connection();

            using (SqlCommand qr = new SqlCommand("sp_GetProducts", con))
            {
                qr.CommandType = CommandType.StoredProcedure;
                con.Open();

                using (SqlDataReader reader = qr.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        product p = new product()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Title = reader["Title"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            Description = reader["Description"].ToString(),
                            Category = reader["Category"].ToString(),
                            Image = reader["Image"].ToString(),
                            Rate = Convert.ToDecimal(reader["Rate"]),
                            Count = Convert.ToInt32(reader["Count"])
                        };

                        products.Add(p);
                    }
                }
                return products;
            }
        }

        public void Updateproduct(product product)
        {
            var con = _context.connection();
            using (SqlCommand qr = new SqlCommand("sp_UpdateProduct", con))
            {
                qr.CommandType = CommandType.StoredProcedure;
                qr.Parameters.AddWithValue("@Id", product.Id);
                qr.Parameters.AddWithValue("@Title", product.Title);
                qr.Parameters.AddWithValue("@Price", product.Price);
                qr.Parameters.AddWithValue("@Description", product.Description);
                qr.Parameters.AddWithValue("@Category", product.Category);
                qr.Parameters.AddWithValue("@Image", product.Image);
                qr.Parameters.AddWithValue("@Rate", product.Rate);
                qr.Parameters.AddWithValue("@Count", product.Count);
                con.Open();
                qr.ExecuteNonQuery();
            }
        }
    }
}
