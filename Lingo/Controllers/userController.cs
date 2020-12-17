using Lingo.DTO;
using Lingo.Models;
using Lingo.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Lingo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public UserController(IUserService userservice, IConfiguration config) {
            _userService = userservice;
            _config = config;
        }


        [HttpPost]
        public IActionResult Register([FromBody] UserDto user) {
            if (ModelState.IsValid)
            {
                UserModel userDB = new UserModel(user.Username, user.Password);
                try
                {
                    if (_userService.AddUser(userDB))
                    {
                        return Ok($"User with username : {user.Username} created.");
                    }
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException ex) when ((ex.InnerException as SqlException)?.Number == 2601) {
                    return Conflict("Username already taken");
                }
            }
            return BadRequest(); 
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserDto userTransfer) {

            if (ModelState.IsValid) {
                UserModel userfromDb = _userService.GetUserByUsername(userTransfer.Username);

                //i'm not allowed to catch a nullreference in this block for whatever reason.
                if (userfromDb == null) {
                    return Unauthorized();
                }

                if (BCrypt.Net.BCrypt.Verify(userTransfer.Password, userfromDb.Password))
                {
                    return Ok(new { token = GenerateJsonWebToken(userfromDb) });
                }
                else {
                    return Unauthorized();
                }
            }
            return BadRequest();
        }


        //https://www.c-sharpcorner.com/article/jwt-json-web-token-authentication-in-asp-net-core/
        private string GenerateJsonWebToken(UserModel userInfo)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt_Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims = new[] {
                new Claim(ClaimTypes.Name, userInfo.Username)
            };

            JwtSecurityToken token = new JwtSecurityToken(_config["Jwt_Issuer"],
              _config["Jwt_Key"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
