using Lingo.Data.Interfaces;
using Lingo.Models;
using System.Collections.Generic;
using System.Linq;

namespace Lingo.Data.Repository
{
    public class HighScoreRepository : IHighscoreRepo
    {
        private readonly LingoContext _context;
        public HighScoreRepository(LingoContext lingoContext) {
            _context = lingoContext;
        }

        public void AddHighscore(HighScoreModel highscore)
        {
            _context.HighScores.Add(highscore);
        }

        public HighScoreModel GetHighScoreModel(HighScoreModel highScoreModel)
        {
            return _context.HighScores.FirstOrDefault(h => h.Score == highScoreModel.Score && h.User.Equals(highScoreModel.User));
        }

        public List<HighScoreModel> GetTopHighscores(int top)
        {
            return _context.HighScores.OrderByDescending(h => h.Score).Take(top).ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
