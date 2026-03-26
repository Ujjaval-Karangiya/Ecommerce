using Microsoft.Data.SqlClient;
using Shoping_Karo.Data;
using Shoping_Karo.DTOs;
using Shoping_Karo.Models;
using Shoping_Karo.Services.Interface;
using System.Data;
using System.Threading.Tasks;

namespace Shoping_Karo.Services.Implementation
{
    public class AuthServices : IAuth
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AuthServices> _logger;

        public AuthServices(AppDbContext context, ILogger<AuthServices> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Register(users user)
        {
            using var con = _context.connection();
            using var cmd = new SqlCommand("sp_userInsert", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Name", user.name);
            cmd.Parameters.AddWithValue("@Email", user.email);
            cmd.Parameters.AddWithValue("@Phone", user.phone);
            cmd.Parameters.AddWithValue("@Address", user.address);
            cmd.Parameters.AddWithValue("@bio", user.bio);
            cmd.Parameters.AddWithValue("@DOB", user.dob ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@password", BCrypt.Net.BCrypt.HashPassword(user.password));

            con.Open();
            cmd.ExecuteNonQuery();
        }

        public void UpdateUser(users user)
        {
            using var con = _context.connection();
            using var cmd = new SqlCommand("sp_UpdateUser", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Id", user.Id);
            cmd.Parameters.AddWithValue("@Name", user.name);
            cmd.Parameters.AddWithValue("@Email", user.email);
            cmd.Parameters.AddWithValue("@Phone", user.phone);
            cmd.Parameters.AddWithValue("@Address", user.address);
            cmd.Parameters.AddWithValue("@bio", user.bio);
            cmd.Parameters.AddWithValue("@DOB", user.dob ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@password", BCrypt.Net.BCrypt.HashPassword(user.password));

            con.Open();
            cmd.ExecuteNonQuery();
        }

        public void DeleteUser(int id)
        {
            using var con = _context.connection();
            using var cmd = new SqlCommand("sp_DeleteUser", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public async Task<LoginDto> login(LoginDto dto)
        {
            using var con = _context.connection();
            using var cmd = new SqlCommand("sp_LoginUser", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@email", dto.email);

            con.Open();
            using var reader = await cmd.ExecuteReaderAsync();

            if (!reader.Read())
            {
                _logger.LogInformation("Login failed - user not found: {Email}", dto.email);
                return null;
            }

            string hashedPassword = reader["password"].ToString();
            if (!BCrypt.Net.BCrypt.Verify(dto.password, hashedPassword))
            {
                _logger.LogInformation("Login failed - invalid password: {Email}", dto.email);
                return null;
            }

            return new LoginDto
            {
                email = reader["email"].ToString()
            };
        }
    }
}