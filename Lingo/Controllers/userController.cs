using Lingo.Data.Interfaces;
using Lingo.DTO;
using Lingo.Models;
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
        private readonly IUserRepo _repository;
        private readonly IConfiguration _config;

        public userController(IUserRepo repository, IConfiguration config) {
            _repository = repository;
            _config = config;
        }


        [HttpPost]
        public ActionResult register([FromBody] userDto user) {
            if (ModelState.IsValid)
            {
                userModel userDB = new userModel(user.username, user.password);
                try
                {
                    _repository.addUser(userDB);
                    if (_repository.saveChanges())
                    {
                        return Ok(user);
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
                userModel userfromDb = _repository.getUserByUsername(userTransfer.username);

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
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username)
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
