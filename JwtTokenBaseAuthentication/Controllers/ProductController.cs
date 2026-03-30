using JwtTokenBaseAuthentication.DTO;
using JwtTokenBaseAuthentication.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtTokenBaseAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(ProductCreateDto productCreateDto)
        {
            var result = await _productService.Create(productCreateDto);
            return Ok(result);
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]

        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


        [HttpPut]
        [Authorize(Roles = "MainUser")]
        public async Task<IActionResult> Update(updateProductDto updateProductDto)
        {
            var result = await _productService.Update(updateProductDto);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.Delete(id);
            if (result == "Product not found")
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }

}
