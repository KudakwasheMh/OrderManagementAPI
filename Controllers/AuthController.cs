using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OrderManagementAPI.Models;
using OrderManagementAPI.Models.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace OrderManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IRep _repository;
        private readonly IConfiguration _config;

        public AuthController(UserManager<User> userManager, IRep repository, IConfiguration configuration)
        {
            _repository = repository;
            _userManager = userManager;
            _config = configuration;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterVm rvm)
        {
            var user = await _userManager.FindByIdAsync(rvm.Email);

            if (user == null)
            {
                user = new Client
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = rvm.UserName,
                    Email = rvm.Email,
                    PhoneNumber = rvm.PhoneNumber,
                    ClientName = rvm.ClientName,
                    LastName = rvm.ClientSurname,

                };
                var result = await _userManager.CreateAsync(user, rvm.Password);

                if (result.Errors.Count() > 0) return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error. Please contact support.");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Client"); // Assign role to new user
                    return Ok(new { message = "Client registered successfully" });
                }
                else if
                   (result.Errors.Count() > 0) return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error. Please contact support.");


            }
            else
            {
                return Forbid("Account already exists.");
            }

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginVm lvm)
        {
            var user = await _userManager.FindByNameAsync(lvm.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, lvm.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var Claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName!)
                };

                Claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

                // Check if the user is a client or a vendor and add the appropriate claim
                if (user is Client client)
                {
                    Claims.Add(new Claim("ClientId", client.ClientId!));
                }
                else if (user is Vendor vendor)
                {
                    Claims.Add(new Claim("VendorId", vendor.VendorId!));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]!));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                issuer: _config["Tokens:Issuer"],
                audience: _config["Tokens:Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
                );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            return Unauthorized();


        }
    }
}

