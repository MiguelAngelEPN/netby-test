using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using product_service.Dto;
using product_service.Models;
using product_service.Service;

namespace product_service.Controllers
{
    [ApiController]
    [Route("api/product-service/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getproductslist")]
        public async Task<IActionResult> GetProducts([FromQuery] int page = 1, [FromQuery] int amount = 10)
        {
            var result = await _productService.GetListProduct(page, amount);
            return StatusCode(result.Code, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProduct(id);
            return StatusCode(result.Code, result);
        }

        [HttpPost("registerproduct")]
        public async Task<IActionResult> RegisterProduct([FromBody] ProductoDto dto)
        {
            var result = await _productService.RegisterProduct(dto);
            return StatusCode(result.Code, result);
        }

        [HttpPut("updateproduct")]
        public async Task<IActionResult> RegisterProduct([FromBody] Producto product)
        {
            var result = await _productService.UpsateProduct(product);
            return StatusCode(result.Code, result);
        }
    }
}
