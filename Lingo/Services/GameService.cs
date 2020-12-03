using Lingo.Data.Interfaces;
using Lingo.Models;
using Lingo.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo.Services
{
    public class GameService : IGameService
    {
        private readonly IUserService _userService;
        private readonly IGameRepo _gameRepo;
        private readonly IWordsRepo _wordsRepo;

        public GameService(IUserService userService, IGameRepo gameRepo, IWordsRepo wordsRepo) {
            _userService = userService;
            _gameRepo = gameRepo;
            _wordsRepo = wordsRepo;
        }

        /// <summary>
        /// method generates a new gameSession instance for the user,
        /// if they user has an existing session reset it to a new session.
        /// </summary>
        /// <param name="Username"></param>
        /// <returns>gameSessionModel</returns>
        public gameSessionModel createNewGameForUser(string Username)
        {
            // this is used to know wether we need to use the update or the add method as using the
            // add method with an existing session will cause a primary key duplicate.
            bool fromDb = true;

            gameSessionModel gameSession = _gameRepo.getCurrentGame(Username);

            if (gameSession == null) {
                gameSession = new gameSessionModel();
                fromDb = false;
            }

            gameSession.player = _userService.getUserByUsername(Username);
            gameSession.currentword = _wordsRepo.GetFiveLetterWord().word;
            gameSession.Guesses = 0;
            gameSession.lastGuess = DateTime.Now;
            gameSession.Score = 0;

            if (fromDb)
            {
                _gameRepo.updateGameSession(gameSession);

            }
            else {
                _gameRepo.addGameSession(gameSession);
            }

            if (_gameRepo.saveChanges()) {
                return gameSession;
            }

            return null;
        }


        public List<List<char>> attemptGuess(string Username, string guessWord) {
            gameSessionModel currentgame = _gameRepo.getCurrentGame(Username);
            return guessVerifier.checkResult(currentgame.currentword, guessWord);
        }

        public bool gameOver(string Username) {
            gameSessionModel currentgame = _gameRepo.getCurrentGame(Username);
            return currentgame.Guesses < 5;
        }

        public bool inTime(string Username) {
            gameSessionModel currentgame = _gameRepo.getCurrentGame(Username);
            return currentgame.guessedIntime(new DateTime());
        }
    }
}
