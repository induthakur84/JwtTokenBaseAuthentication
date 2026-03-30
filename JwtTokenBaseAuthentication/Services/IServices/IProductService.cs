using JwtTokenBaseAuthentication.DTO;

namespace JwtTokenBaseAuthentication.Services.IServices
{
    public interface IProductService
    {
        Task<ProductResponseDto> Create(ProductCreateDto productCreateDto);

        Task<ProductResponseDto> GetById(int id);
        Task<List<ProductResponseDto>> GetAll();
        Task<ProductResponseDto> Update(updateProductDto updateProductDto);
        Task<string> Delete(int id);
    }
}
