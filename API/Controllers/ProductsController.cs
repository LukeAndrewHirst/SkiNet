using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController(IUnitOfWork unitOfWork) : BaseApiController
    {
        [Cache(10000)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery]ProductSpecificationParams specificationParams)
        {
            var spec = new ProductSpecification(specificationParams);

            return await CreatePagedResult(unitOfWork.Repository<Product>(), spec, specificationParams.PageIndex, specificationParams.PageSize);
        }

        [Cache(10000)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await unitOfWork.Repository<Product>().GetByIdAsync(id);
            if(product == null) return NotFound();
            
            return product;
        }

        [Cache(10000)]
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            var spec = new BrandListSpecification();

            return Ok(await unitOfWork.Repository<Product>().ListAsync(spec));
        }

        [Cache(10000)]
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec = new TypeListSpecification();

            return Ok(await unitOfWork.Repository<Product>().ListAsync(spec));
        }

        [InvalidateCache("api/products|")]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            unitOfWork.Repository<Product>().Add(product);

            if(await unitOfWork.Complete()) return CreatedAtAction("GetProduct", new {id = product?.Id}, product);

            return BadRequest("Unable to create new product");
        }
        
        [InvalidateCache("api/products|")]
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if(product.Id != id || !ProductExisits(id)) return BadRequest("Update to update this product");

            unitOfWork.Repository<Product>().Update(product);

            if(await unitOfWork.Complete()) return NoContent();

            return BadRequest("Unable to update product");
        }
        
        [InvalidateCache("api/products|")]
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await unitOfWork.Repository<Product>().GetByIdAsync(id);
            if(product == null) return NotFound();

            unitOfWork.Repository<Product>().Remove(product);

            if(await unitOfWork.Complete()) return NoContent();

            return BadRequest("Unable to delete product");
        }

        private bool ProductExisits(int id)
        {
            return unitOfWork.Repository<Product>().Exists(id);
        }
    }
}