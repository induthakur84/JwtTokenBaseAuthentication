using JwtTokenBaseAuthentication.DTO;
using JwtTokenBaseAuthentication.Models;
using JwtTokenBaseAuthentication.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtTokenBaseAuthentication.Services
{
    public class UserService : IUserService


    {
        // this is the database context to interact with db
        private readonly ApplicationDbContext _context;
        //this is used to read values for appsetting.json file
        private readonly IConfiguration _configuration;

        public UserService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        //Register User
        public async Task<UserResponseDto> Register(UserRegisterDto userRegisterDto)
        {

            //Create a new use object
            var user = new User
            {
                Name = userRegisterDto.Name,
                Username = userRegisterDto.Username,

                //Admin@123
                //we can convert to hash password before saveing the dat
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userRegisterDto.Password),
                Role = userRegisterDto.Role,
            };
            // add user in the database
            await _context.Users.AddAsync(user);
            //here we save changes asynchonously
            await _context.SaveChangesAsync();
            //return save user data(Without password)
            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Role = user.Role,
            };
        }

        public async Task<LoginResponseDto> Login(LoginDto loginDto)
        {
            ///Find user by username
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == loginDto.Username);


            //if User not found that throl error

            if (user == null)
                throw new Exception("User not found");
            // verify or test the entered password with stored hashpassworod
            bool isvalid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);

                    //if password is incorrect then throw error
                    if (!isvalid)
                throw new Exception("Invalid Password");
            var token = GenerateToken(user);


            return new LoginResponseDto
            {
                Token = token,
                User = new UserResponseDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Username = user.Username,
                    Role = user.Role,
                }
            };

        }


        #region private method
        private string GenerateToken(User user)
        {

            //we can Read Jwt Setting from appsetting.json file


            var jwtSettings = _configuration.GetSection("Jwt");



            //here we convert secert key in byte  array

            var key = new SymmetricSecurityKey(

               Encoding.UTF8.GetBytes(jwtSettings["key"])
               );



            //Create signing credientials using key +Algoritm
            var creds=  new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            //payload



            //it defines the user claims( here we can user data inside the token) 
            var claim = new[]
            {

                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("userId", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("Username", user.Username),  
            };



            //signature
            //Create Jwt Token

            var token = new JwtSecurityToken(

                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims:claim,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpireTimeInMiutes"])),
                signingCredentials: creds   
                );
            
            //covert token object to string and return
            return new JwtSecurityTokenHandler().WriteToken(token); 

        }
        #endregion

    }
}
