using Microsoft.AspNetCore.Mvc;

using Shoping_Karo.Models;
using Shoping_Karo.Services.Interface;

namespace Shoping_Karo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _product;

        public ProductController(IProduct product)
        {
            _product = product;
        }

        // GET: api/Product
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var data = _product.showProduct();
            return Ok(data);
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var data = _product.getProductById(id);
            return Ok(data);
        }

        // POST: api/Product
        [HttpPost]
        public IActionResult AddProduct([FromBody] product product)
        {
            _product.Addproduct(product);
            return Ok("Product Added Successfully");
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] product product)
        {
            product.Id = id;
            _product.Updateproduct(product);
            return Ok("Product Updated Successfully");
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            _product.Deleteproduct(id);
            return Ok("Product Deleted Successfully");
        }
    }
}