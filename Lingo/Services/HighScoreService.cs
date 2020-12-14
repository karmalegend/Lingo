using Lingo.Data.Interfaces;
using Lingo.Models;
using Lingo.Services.Interfaces;
using System.Collections.Generic;

namespace Lingo.Services
{
    public class HighScoreService : IHighScoreService
    {
        private readonly IHighscoreRepo _highScoreRepo;

        public HighScoreService(IHighscoreRepo highscoreRepository) {
            _highScoreRepo = highscoreRepository;
        }

        public bool AddNewHighScore(HighScoreModel highScore)
        {
            if (_highScoreRepo.GetHighScoreModel(highScore) == null)
            {
                _highScoreRepo.AddHighscore(highScore);
            }
            return _highScoreRepo.SaveChanges();
        }

        public List<HighScoreModel> GetHighScores(int top)
        {
            return _highScoreRepo.GetTopHighscores(top);
        }
    }
}
