using JwtTokenBaseAuthentication.DTO;
using JwtTokenBaseAuthentication.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtTokenBaseAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]

        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            var result = await _userService.Register(userRegisterDto);
            if (result == null)
            {
                return BadRequest("User registration failed.");
            }
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                var result = await _userService.Login(loginDto);


                return Ok(new
                {
                    message = "Login successful.",
                    token = result.Token,
                    user = result.User
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Login failed.",
                    error = ex.Message
                });

            }
        }
    }
}
