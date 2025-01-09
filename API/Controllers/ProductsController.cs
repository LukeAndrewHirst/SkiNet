using System.Diagnostics;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IProductRepository productRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type, string? sort)
        {
            return Ok(await productRepository.GetProductsAsync(brand, type, sort));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await productRepository.GetProductByIdAsync(id);
            if(product == null) return NotFound();
            
            return product;
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            return Ok(await productRepository.GetBrandsAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            return Ok(await productRepository.GetTypesAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            productRepository.AddProduct(product);

            if(await productRepository.SaveChangesAsync()) return CreatedAtAction("GetProduct", new {id = product?.Id}, product);

            return BadRequest("Unable to create new product");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if(product.Id != id || !ProductExisits(id)) return BadRequest("Update to update this product");

            productRepository.UpdateProduct(product);

            if(await productRepository.SaveChangesAsync()) return NoContent();

            return BadRequest("Unable to update product");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await productRepository.GetProductByIdAsync(id);
            if(product == null) return NotFound();

            productRepository.DeleteProduct(product);

            if(await productRepository.SaveChangesAsync()) return NoContent();

            return BadRequest("Unable to delete product");
        }

        private bool ProductExisits(int id)
        {
            return productRepository.ProductExists(id);
        }
    }
}