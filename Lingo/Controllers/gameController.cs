using AutoMapper;
using Lingo.DTO;
using Lingo.Models;
using Lingo.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Lingo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public GameController(IGameService gameService, IMapper mapper) {
            _gameService = gameService;
            _mapper = mapper;
        }

        /// <summary>
        /// Registering a new game for the user.
        /// </summary>
        /// <returns> http response.</returns>
        [HttpGet("new")]
        public IActionResult CreateNewGameSession() {
            GameSessionModel created = _gameService.CreateNewGameForUser(User.Identity.Name);
            if (created != null)
            {
                return Ok(_mapper.Map<GameSessionDtoRead>(created));
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult GuessWord(GuessWriteDto guessWrite)
        {
            // verify if all valid preconditions are met
            GameSessionModel userGame = _gameService.RetrieveGameSessionModelByUsername(User.Identity.Name);

            if (!ModelState.IsValid) {
                return BadRequest();
            }

            if (userGame == null) {
                return Conflict("User doesn't currently have any game.");
            }
            string score = $"\n You're score this game was {userGame.Score}";
            if (_gameService.GameOver(userGame)) {
                return Ok("You've reached the maximum ammount of guesses please create a new game and try again." + score);
            }
            if (!_gameService.InTime(userGame)) {
                if (_gameService.IncrementGuessCounter(userGame)) {
                    return Ok("You've waited too long your guess counter got incremented to the max ammount of guesses please create a new game" + score);
                }
                return Ok("You've waited too long your guess counter has been incremented please try again.");
            }
            if (!_gameService.MatchingWordLengths(userGame,guessWrite.Guess)) {
                return Ok("Your guessed word and the current word do not match in length.");
            }

            //progress the actual guess verification here.
            List<List<char>> results = _gameService.AttemptGuess(userGame, guessWrite.Guess);
            if (_gameService.CorrectGuess(results[1]))
            {
                return Ok($"Congratulations you've correctly guessed the word! a new {_gameService.GetNewWordForGame(userGame)} letter word has been selected.");
            }

            if(!_gameService.CorrectGuess(results[1])) {
                return Ok(results);
            }

            return BadRequest();
        }
    }
}
