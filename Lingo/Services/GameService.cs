using Lingo.Data.Interfaces;
using Lingo.Models;
using Lingo.Services.Interfaces;
using System;
using System.Collections.Generic;

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
        public GameSessionModel CreateNewGameForUser(string Username)
        {
            // this is used to know wether we need to use the update or the add method as using the
            // add method with an existing session will cause a primary key duplicate.
            bool fromDb = true;

            GameSessionModel gameSession = _gameRepo.GetCurrentGame(Username);

            if (gameSession == null) {
                gameSession = new GameSessionModel();
                fromDb = false;
            }

            gameSession.Player = _userService.GetUserByUsername(Username);
            gameSession.Currentword = _wordsRepo.GetFiveLetterWord().Word;
            gameSession.Guesses = 0;
            gameSession.LastGuess = DateTime.Now;
            gameSession.Score = 0;

            if (fromDb)
            {
                _gameRepo.UpdateGameSession(gameSession);
                _gameRepo.SaveChanges();

            }
            else {
                _gameRepo.AddGameSession(gameSession);
                _gameRepo.SaveChanges();
            }

            if (_gameRepo.SaveChanges()) {
                return gameSession;
            }

            return null;
        }

        public GameSessionModel RetrieveGameSessionModelByUsername(string username) {
            return _gameRepo.GetCurrentGame(username);
        }

        public bool CorrectGuess(List<char> results) {
            return !results.Contains('P') && !results.Contains('A');
        }

        public bool MatchingWordLengths(GameSessionModel currentgame, string guessWord) {
            return currentgame.Currentword.Length == guessWord.Length;
        }


        public List<List<char>> AttemptGuess(GameSessionModel currentgame, string guessWord) {
            IncrementGuessCounter(currentgame);
            return GuessVerifier.CheckResult(currentgame.Currentword, guessWord);
        }

        public bool GameOver(GameSessionModel currentgame) {
            bool over = currentgame.Guesses >= 5;
            if (over) {
                _highScoreService.AddNewHighScore(new HighScoreModel(currentgame.Score,currentgame.Player.Username));
            }
            return over;
        }

        public bool InTime(GameSessionModel currentgame) {
            bool intime = currentgame.GuessedIntime(DateTime.Now);
            currentgame.LastGuess = DateTime.Now;
            _gameRepo.UpdateGameSession(currentgame);
            _gameRepo.SaveChanges();
            return intime;
        }

        public bool IncrementGuessCounter(GameSessionModel currentgame) {
            currentgame.IncreaseGuess();
            _gameRepo.UpdateGameSession(currentgame);
            _gameRepo.SaveChanges();
            return GameOver(currentgame);
        }

        public int GetNewWordForGame(GameSessionModel currentgame)
        {
            int points = 0;
            switch (currentgame.Currentword.Length) {
                case 5:
                    currentgame.Currentword = _wordsRepo.GetSixLetterWord().Word;
                    points = 1;
                    break;
                case 6:
                    currentgame.Currentword = _wordsRepo.GetSevenLetterWord().Word;
                    points = 3;
                    break;
                case 7:
                    currentgame.Currentword = _wordsRepo.GetFiveLetterWord().Word;
                    points = 5;
                    break;

            }

            currentgame.Score = currentgame.Score + points;
            currentgame.Guesses = 0;
            currentgame.LastGuess = DateTime.Now;
            _gameRepo.UpdateGameSession(currentgame);
            _gameRepo.SaveChanges();
            return currentgame.Currentword.Length;
        }
    }
}
