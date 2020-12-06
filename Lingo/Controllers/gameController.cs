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
        public IActionResult guessWord(guessWriteDto guessWrite)
        {
            // verify if all valid preconditions are met
            gameSessionModel userGame = _gameService.retrieveGameSessionModelByUsername(User.Identity.Name);

            if (!ModelState.IsValid) {
                return BadRequest();
            }

            string score = $"\n You're score this game was {userGame.Score}"; 

            if (userGame == null) {
                return Conflict("User doesn't currently have any game.");
            }
            if (_gameService.gameOver(userGame)) {
                return Ok("You've reached the maximum ammount of guesses please create a new game and try again." + score);
            }
            if (!_gameService.inTime(userGame)) {
                if (_gameService.incrementGuessCounter(userGame)) {
                    return Ok("You've waited too long your guess counter got incremented to the max ammount of guesses please create a new game" + score);
                }
                return Ok("You've waited too long your guess counter has been incremented please try again.");
            }
            if (!_gameService.matchingWordLengths(userGame,guessWrite.guess)) {
                return Ok("Your guessed word and the current word do not match in length.");
            }

            //progress the actual guess verification here.
            List<List<char>> results = _gameService.attemptGuess(userGame, guessWrite.guess);
            if (_gameService.correctGuess(results[1]))
            {
                return Ok($"Congratulations you've correctly guessed the word! a new {_gameService.getNewWordForGame(userGame)} letter word has been selected.");
            }
            else if(!_gameService.correctGuess(results[1])) {
                return Ok(results);
            }

            return BadRequest();
        }
    }
}
