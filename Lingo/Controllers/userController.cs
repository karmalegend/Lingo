using Lingo.Data.Interfaces;
using Lingo.DTO;
using Lingo.Models;
using Lingo.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Lingo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public userController(IUserService userservice, IConfiguration config) {
            _userService = userservice;
            _config = config;
        }


        [HttpPost]
        public IActionResult register([FromBody] userDto user) {
            if (ModelState.IsValid)
            {
                userModel userDB = new userModel(user.username, user.password);
                try
                {
                    if (_userService.addUser(userDB))
                    {
                        return Ok($"User with username : {user.username} created.");
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
        public IActionResult login([FromBody] userDto userTransfer) {

            if (ModelState.IsValid) {
                userModel userfromDb = _userService.getUserByUsername(userTransfer.username);

                //i'm not allowed to catch a nullreference in this block for whatever reason.
                if (userfromDb == null) {
                    return Unauthorized();
                }

                if (BCrypt.Net.BCrypt.Verify(userTransfer.password, userfromDb.Password))
                {
                    return Ok(new { token = GenerateJSONWebToken(userfromDb) });
                }
                else {
                    return Unauthorized();
                }
            }
            return BadRequest();
        }


        //https://www.c-sharpcorner.com/article/jwt-json-web-token-authentication-in-asp-net-core/
        private string GenerateJSONWebToken(userModel userInfo)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims = new[] {
                new Claim(ClaimTypes.Name, userInfo.Username)
            };

            JwtSecurityToken token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
