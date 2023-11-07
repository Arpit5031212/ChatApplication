using ChatApp.Business.Helpers;
using ChatApp.Business.ServiceInterfaces;
using ChatApp.Context.EntityClasses;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly IConfiguration _config;

        public AccountController(IConfiguration config, IProfileService profileService)
        {
            _config = config;
            _profileService = profileService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(RegisterModel registerObj)
        {
            var user = await _profileService.RegisterUser(registerObj);

            if(user != null)
            {
                var token = GenerateJsonWebToken(user);
                return Ok( new { token = token, user = user });
            }

            return BadRequest( new { Message = "User Already Exists. Please use different email and Username." });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            IActionResult response = Unauthorized(new { Message = "Invalid Credentials."});

            var user = await _profileService.CheckPassword(loginModel);

            if (user != null) 
            {
                var token = GenerateJsonWebToken(user);
                response = Ok(new { token, user.Id });
            }

            return response;
        }

        
        //public async Task<IActionResult> GetUser(int id)
        //{
        //    var UserProfile
        //}

        

        private string GenerateJsonWebToken(UserProfile user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("4D2P7ZNN5tMxkJA12xajnbdcakj"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(ClaimsConstant.FirstNameClaim, user.FirstName),
                    new Claim(ClaimsConstant.LastNameClaim, user.LastName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(_config["Jwt: Issuer"],
                _config["Jwt: Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
