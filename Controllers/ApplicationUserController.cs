using Azure;
using ChampagneApi.Data;
using ChampagneApi.Models;
using ChampagneApi.Models.ApplicationUser;
using ChampagneApi.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ChampagneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ApplicationUserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _signInManager = signInManager;
        }


        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {           
            try
            {
                //var response = await _userManager.Users.ToListAsync();
                var response = await _userManager.Users.Select(x => x.Email).ToListAsync(); //to just get the email
                if(response.Count == 0) return Ok("No data found");
                else return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
        {
            try
            {
                var response = await _userManager.Users.Where(x => x.Email == email).ToListAsync();
                if (response.Count == 0) return Ok("No data found with this email");
                else return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel) //[FromBody] means look info from the body not the url
        { 
            var userToBeCreated = new ApplicationUser
            {
                FirstName = signUpModel.FirstName,
                LastName = signUpModel.LastName,
                Email = signUpModel.Email,
                UserName = signUpModel.Email,
            };

            var response = await _userManager.CreateAsync(userToBeCreated, signUpModel.Password);

            if(response.Succeeded) 
            { 
                return Ok("User created");
            }
            else
            {
                return BadRequest(response.Errors);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginUserModel)
        {
            var user = await _userManager.FindByEmailAsync(loginUserModel.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginUserModel.Password))
            {
                return Ok("Login successful");
            }

            return Unauthorized("Invalid email or password");
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(DeleteUserModel deleteUserModel)
        {
            var existingUser = await _userManager.FindByEmailAsync(deleteUserModel.Email);

            if (existingUser != null)
            {
                var response = await _userManager.DeleteAsync(existingUser);

                return response.Succeeded ? Ok("User deleted") : BadRequest(response.Errors);
            }
            else
            {
                return BadRequest("No user found with this email");
            }
        }
        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                var response = await _roleManager.Roles.ToListAsync();
                if (response.Count == 0) return Ok("No data found");
                else return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(CreateRoleModel createRoleModel)
        {
            var response = await _roleManager.CreateAsync(new IdentityRole
            {
                Name = createRoleModel.RoleName
            });

            return response.Succeeded ? Ok("Role created") : BadRequest(response.Errors);
        }

        [HttpDelete("DeleteRole")]
        public async Task<IActionResult> DeleteRole([FromBody] CreateRoleModel deleteUserModel)
        {
            var esixtingRole = await _roleManager.FindByNameAsync(deleteUserModel.RoleName);

            if (esixtingRole != null)
            {
                var response = await _roleManager.DeleteAsync(esixtingRole);

                if (response.Succeeded)
                {
                    return Ok("Role Deleted");
                }
                else
                {
                    return BadRequest(response.Errors);
                }
            }
            else
            {
                return BadRequest("No role found with this name");
            }
        }

        [HttpPost("AssignRoleToUser")]
        public async Task<IActionResult> AssignRoleToUser(RoleToUserModel assignRoleToUserModel)
        {
            var userDetails = await _userManager.FindByEmailAsync(assignRoleToUserModel.Email);

            if(userDetails != null)
            {
                var response = await _userManager.AddToRoleAsync(userDetails, assignRoleToUserModel.RoleName);

                return response.Succeeded ? Ok("Role assigned to user") : BadRequest(response.Errors);
            }
            else
            {
                return BadRequest("No user with this email");
            }
        }

        [HttpDelete("RemoveRoleToUser")]
        public async Task<IActionResult> RemoveRoleToUser([FromBody] RoleToUserModel removeRoleToUserModel)
        {
            var userDetails = await _userManager.FindByEmailAsync(removeRoleToUserModel.Email);

            if (userDetails != null)
            {
                var response = await _userManager.RemoveFromRoleAsync(userDetails, removeRoleToUserModel.RoleName);

                if (response.Succeeded)
                {
                    return Ok($"Role: {removeRoleToUserModel.RoleName} removed from {removeRoleToUserModel.Email}");
                }
                else
                {
                    return BadRequest(response.Errors);
                }
            }
            else
            {
                return BadRequest("No user found with this email");
            }
        }




    }
}
