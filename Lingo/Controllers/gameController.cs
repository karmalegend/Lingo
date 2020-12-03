using AutoMapper;
using Lingo.DTO;
using Lingo.Models;
using Lingo.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lingo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class gameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public gameController(IGameService gameService, IMapper mapper) {
            _gameService = gameService;
            _mapper = mapper;
        }

        /// <summary>
        /// Registering a new game for the user.
        /// </summary>
        /// <returns> http response.</returns>
        [HttpGet("new")]
        public IActionResult createNewGameSession() {
            gameSessionModel created = _gameService.createNewGameForUser(User.Identity.Name);
            if (created != null)
            {
                return Ok(_mapper.Map<gameSessionDtoRead>(created));
            }
            else {
                return BadRequest();
            }
        }

        [HttpGet]
        public List<List<char>> guessWord(guessWriteDto guessWrite)
        {
            return null;
        }
    }
}
