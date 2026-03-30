using JwtTokenBaseAuthentication.DTO;
using JwtTokenBaseAuthentication.Models;
using JwtTokenBaseAuthentication.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace JwtTokenBaseAuthentication.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ProductResponseDto> Create(ProductCreateDto productCreateDto)
        {
           var product = new Product
            {
                Name = productCreateDto.Name,
                Description = productCreateDto.Description,
                Author = productCreateDto.Author,
                Price = productCreateDto.Price,
                Quntity = productCreateDto.Quntity
            };
            _context.Products.Add(product);
          await  _context.SaveChangesAsync();
            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            }; 
        }

        public async Task<string> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return "Product not found";
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();   
            return "Product deleted successfully";
        }

        public  async Task<List<ProductResponseDto>> GetAll()
        {
           return  await _context.Products.Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            }).ToListAsync();
        }

        public async Task<ProductResponseDto> GetById(int id)
        {
          var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }
            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }

        public async Task<ProductResponseDto> Update(updateProductDto updateProductDto)
        {
           var product = await _context.Products.FindAsync(updateProductDto.Id);
            if (product == null)
            {
                return null;
            }
            product.Name = updateProductDto.Name;
            product.Description = updateProductDto.Description;
            product.Author = updateProductDto.Author;
            product.Price = updateProductDto.Price;
            product.Quntity = updateProductDto.Quntity;

            await _context.SaveChangesAsync();

            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }
    }
}
