using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _config;
        public LoginController(IConfiguration config, SignInManager<IdentityUser> signInManager) 
        { 
            _signInManager = signInManager;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] User loginModel)
        {
            IActionResult response = Unauthorized();
            var success = AuthenticateUser(loginModel);
            if (success)
            {
                var tokenString = GenerateJsonWebToken(loginModel);
                response = Ok(new { token = tokenString });
            }
            return response;
        }

        private object GenerateJsonWebToken(User loginModel)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Issuer"], null, expires: DateTime.Now.AddMinutes(120),signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool AuthenticateUser(User loginModel)
        {
            var result = _signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, true, lockoutOnFailure: false).Result;
            return result.Succeeded;
        }
    }
}
