using ChampagneApi.Data;
using ChampagneApi.Models;
using ChampagneApi.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ChampagneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppAuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AppAuthenticationController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }



        [AllowAnonymous]
        [HttpPost("AuthenticateUser")]
        public async Task<IActionResult> AuthenticateUser(AuthenticateUserModel autenticateUserModel)
        {
            var user = await _userManager.FindByEmailAsync(autenticateUserModel.Email);

            if (user == null) return Unauthorized();

            bool isValidUser = await _userManager.CheckPasswordAsync(user, autenticateUserModel.Password);

            if (isValidUser)
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var keyDetails = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    //new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Audience = _configuration["JWT:Audience"],
                    Issuer = _configuration["Jwt:Issuer"],
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    Subject = new ClaimsIdentity(claims),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyDetails), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(tokenHandler.WriteToken(token));
            }
            else
            {
                return Unauthorized();
            }
        }


        [HttpPost("AuthLogin")]
        public IActionResult AuthLogin(AuthenticateUserModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid client request");
            }

            if (model.Email == "cindy@test.com" && model.Password == "cindy")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok($"Bearer {tokenString}");
            }
            else
            {
                return Unauthorized();
            }
        }



    }
}
