
using Shoping_Karo.DTOs;
using Shoping_Karo.Models;

namespace Shoping_Karo.Services.Interface
{
    public interface IAuth
    {
        void Register(users user);
        void UpdateUser(users user);
        void DeleteUser(int id);
        Task<LoginDto> login(LoginDto Dto);
    }
}
