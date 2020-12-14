using Lingo.Models;
using System.Collections.Generic;

namespace Lingo.Data.Interfaces
{
    public interface IHighscoreRepo
    {
        public void AddHighscore(HighScoreModel highscore);
        public List<HighScoreModel> GetTopHighscores(int top);
        public HighScoreModel GetHighScoreModel(HighScoreModel highScoreModel);
        public bool SaveChanges();
    }
}
