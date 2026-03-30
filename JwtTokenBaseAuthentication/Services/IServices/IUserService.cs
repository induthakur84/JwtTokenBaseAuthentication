using JwtTokenBaseAuthentication.DTO;
using JwtTokenBaseAuthentication.Models;

namespace JwtTokenBaseAuthentication.Services.IServices
{
    public interface IUserService
    {
        Task<UserResponseDto> Register(UserRegisterDto userRegisterDto);
        Task<LoginResponseDto> Login(LoginDto loginDto);
    }
}
