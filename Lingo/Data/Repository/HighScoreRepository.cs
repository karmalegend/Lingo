using Lingo.Data.Interfaces;
using Lingo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo.Data.Repository
{
    public class HighScoreRepository : IHighscoreRepo
    {
        private readonly LingoContext _context;
        public HighScoreRepository(LingoContext lingoContext) {
            _context = lingoContext;
        }

        public void addHighscore(highScoreModel highscore)
        {
            _context.highScores.Add(highscore);
        }

        public highScoreModel getHighScoreModel(highScoreModel highScoreModel)
        {
            return _context.highScores.Where(h => h.score == highScoreModel.score && h.user.Equals(highScoreModel.user)).FirstOrDefault();
        }

        public List<highScoreModel> getTopHighscores(int top)
        {
            return _context.highScores.OrderByDescending(h => h.score).Take(top).ToList();
        }

        public bool saveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
