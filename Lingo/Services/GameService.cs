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
        private readonly IHighScoreService _highScoreService;
        private readonly IUserService _userService;
        private readonly IGameRepo _gameRepo;
        private readonly IWordsRepo _wordsRepo;

        public GameService(IHighScoreService highscoreservice, IUserService userService, IGameRepo gameRepo, IWordsRepo wordsRepo) {
            _userService = userService;
            _gameRepo = gameRepo;
            _wordsRepo = wordsRepo;
            _highScoreService = highscoreservice;
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
                _gameRepo.saveChanges();

            }
            else {
                _gameRepo.addGameSession(gameSession);
                _gameRepo.saveChanges();
            }

            if (_gameRepo.saveChanges()) {
                return gameSession;
            }

            return null;
        }

        public gameSessionModel retrieveGameSessionModelByUsername(string username) {
            return _gameRepo.getCurrentGame(username);
        }

        public bool correctGuess(List<char> results) {
            return !results.Contains('P') && !results.Contains('A');
        }

        public bool matchingWordLengths(gameSessionModel currentgame, string guessWord) {
            return currentgame.currentword.Length == guessWord.Length;
        }


        public List<List<char>> attemptGuess(gameSessionModel currentgame, string guessWord) {
            incrementGuessCounter(currentgame);
            return guessVerifier.checkResult(currentgame.currentword, guessWord);
        }

        public bool gameOver(gameSessionModel currentgame) {
            bool over = currentgame.Guesses >= 5;
            if (over) {
                _highScoreService.addNewHighScore(new highScoreModel(currentgame.Score,currentgame.player.Username));
            }
            return over;
        }

        public bool inTime(gameSessionModel currentgame) {
            bool intime = currentgame.guessedIntime(DateTime.Now);
            currentgame.lastGuess = DateTime.Now;
            _gameRepo.updateGameSession(currentgame);
            _gameRepo.saveChanges();
            return intime;
        }

        public bool incrementGuessCounter(gameSessionModel currentgame) {
            currentgame.increaseGuess();
            _gameRepo.updateGameSession(currentgame);
            _gameRepo.saveChanges();
            return gameOver(currentgame);
        }

        public int getNewWordForGame(gameSessionModel currentgame)
        {
            int points = 0;
            switch (currentgame.currentword.Length) {
                case 5:
                    currentgame.currentword = _wordsRepo.GetSixLetterWord().word;
                    points = 1;
                    break;
                case 6:
                    currentgame.currentword = _wordsRepo.GetSevenLetterWord().word;
                    points = 3;
                    break;
                case 7:
                    currentgame.currentword = _wordsRepo.GetFiveLetterWord().word;
                    points = 5;
                    break;

            }

            currentgame.Score = currentgame.Score + points;
            currentgame.Guesses = 0;
            currentgame.lastGuess = DateTime.Now;
            _gameRepo.updateGameSession(currentgame);
            _gameRepo.saveChanges();
            return currentgame.currentword.Length;
        }
    }
}
