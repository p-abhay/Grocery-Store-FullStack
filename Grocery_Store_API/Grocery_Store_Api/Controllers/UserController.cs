using BusinessLayer.IServices;
using DTOs.DTOModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Grocery_Store_Api.Controllers
{
    [ApiController]
    [Route("/api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
        [Route("/api/users/signup")]
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] UserDTOModel user)
        {
            user.Id = Guid.NewGuid();
            await _userService.SignUpAsync(user);
            return Ok(user);
        }
        [HttpPost("/api/users/login")]
        public async Task<IActionResult> LogIn([FromBody] LoginModel loginModel)
        {
            var user = await _userService.LogInAsync(loginModel.Username, loginModel.Password);
            if (user == default)
                return BadRequest(user);
            //return Ok(response);

            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = "63797DF2499152D385A89169CED1DA619F9E4C6CB7ED3F1977233AF24B513DA1";
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Name", user.FullName),
                    new Claim("Email", user.Email),
                    new Claim("PhoneNumber", user.PhoneNumber),
                    new Claim("isAdmin", user.isAdmin ? "true" : "false")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            var response = new
            {
                jwt,
                user
            };
            return Ok(response);
            // Return the JWT to the client
            //return Ok(new { jwt });
        }
    }
}
