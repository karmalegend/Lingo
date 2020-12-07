using Lingo.Data.Interfaces;
using Lingo.Models;
using Lingo.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo.Services
{
    public class HighScoreService : IHighScoreService
    {
        private readonly IHighscoreRepo _highScoreRepo;

        public HighScoreService(IHighscoreRepo highscoreRepository) {
            _highScoreRepo = highscoreRepository;
        }

        public bool addNewHighScore(highScoreModel highScore)
        {
            if (_highScoreRepo.getHighScoreModel(highScore) == null)
            {
                _highScoreRepo.addHighscore(highScore);
            }
            return _highScoreRepo.saveChanges();
        }

        public List<highScoreModel> getHighScores(int top)
        {
            return _highScoreRepo.getTopHighscores(top);
        }
    }
}
