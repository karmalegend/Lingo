using Lingo.Data.Interfaces;
using Lingo.DTO;
using Lingo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private readonly IUserRepo _repository;

        public userController(IUserRepo repository) {
            _repository = repository;
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
    }
}
