using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shoping_Karo.Models;

namespace Shoping_Karo.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public AppDbContext(DbContextOptions<AppDbContext> options,IConfiguration configuration) : base(options) 
        { 
           _configuration = configuration;
        }
        public SqlConnection connection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        public DbSet<wishlist> wishlist { get; set; }
        public DbSet<orders> orders { get; set; }
        public DbSet<product> Product { get; set; }
        public DbSet<users> users { get; set; }
        public DbSet<cart> cart { get; set; } = null!;
    }
}
