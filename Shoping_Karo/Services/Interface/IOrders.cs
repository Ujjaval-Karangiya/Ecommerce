using Shoping_Karo.Models;

namespace Shoping_Karo.Services.Interface
{
    public interface IOrder
    {
        void AddOrder(orders order);
        List<orders> GetOrdersByUser(string userId);
    }
}