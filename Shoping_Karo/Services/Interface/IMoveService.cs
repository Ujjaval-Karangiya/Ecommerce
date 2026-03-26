namespace Shoping_Karo.Services.Interface
{
    public interface IMoveService
    {
        void MoveWishlistToCart(string userId);
        void MoveSingleWishlistToCart(int id);
        void MoveCartToOrder(string userId);
        void MoveSingleCartToOrder(string userId, int productId);
    }
}