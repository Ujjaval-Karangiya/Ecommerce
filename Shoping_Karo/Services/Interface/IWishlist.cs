using Shoping_Karo.Models;

namespace Shoping_Karo.Services.Interface
{
    public interface IWishlist
    {
        void addWishlist(wishlist wishlist);
        void deleteWishlistid(int id);
        List<wishlist> getWishlistByUserId(int id);



    }
}


//exec sp_InsertWishlist 2,1,'hp victus',50000,'laptop image';
//exec sp_DeleteWishlist 6;
//exec sp_GetWishlistByUser 4;
//exec sp_clearWishlist;