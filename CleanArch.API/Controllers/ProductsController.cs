using CleanArch.Application.DTOs;
using CleanArch.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await _productService.GetProducts();

            if (products == null)
            {
                return NotFound("Products not found!");
            }

            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetProductById")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var product = await _productService.GetById(id);

            if (product == null)
            {
                return NotFound("Product not found!");
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProduct([FromBody] ProductDTO productDto)
        {
            if (productDto == null)
                return BadRequest("Invalid Data!");

            await _productService.Add(productDto);

            return new CreatedAtRouteResult("GetProductById", new { id = productDto.Id }, productDto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductDTO>> UpdateProduct(int id, [FromBody] ProductDTO productDto)
        {
            if (id != productDto.Id)
                return BadRequest("Invalid Id!");

            if (productDto == null)
                return BadRequest("Invalid Data!");

            await _productService.Update(productDto);

            return Ok(productDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetById(id);

            if (product == null)
                return NotFound("Product not found!");

            await _productService.Remove(id);

            return Ok();
        }
    }
}
