using Shoping_Karo.Models;

namespace Shoping_Karo.Services.Interface
{
    public interface IProduct
    {
        void Addproduct(product product);
        void Updateproduct(product product);
        void Deleteproduct(int id);
        List<product> showProduct();
        List<product> getProductById(int id);
    }
}
//exec sp_InsertProduct 2,'moto edge 40 neo',25000,'slim and flexiable phone','electronics','phone image',4.5,1;
//exec sp_UpdateProduct 3,'redmi 9 power',13000,' flexiable phone','electronics','phone image',4.5,1;
//exec sp_DeleteProduct 4;
//exec sp_GetProducts;
//exec sp_GetProductById 1;
