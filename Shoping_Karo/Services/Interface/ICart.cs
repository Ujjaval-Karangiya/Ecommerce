using Shoping_Karo.Models;

namespace Shoping_Karo.Services.Interface
{
    public interface ICart
    {
        void addToCart(cart cart);
        void DecreaseCartIdQty(int id);
        void DeleteCartId(int id);
        List<cart> getCartByUserId(int id);
    }
}


//exec sp_AddToCart 2,2,'hp victus',50000,1,'laptop image';
//exec sp_DecreaseCartQty 15;
//exec sp_DeleteCart 9;
//exec sp_clearCart;
//exec sp_GetCartByUser 4;
