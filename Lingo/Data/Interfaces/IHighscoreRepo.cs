using Lingo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo.Data.Interfaces
{
    public interface IHighscoreRepo
    {
        public void addHighscore(highScoreModel highscore);
        public List<highScoreModel> getTopHighscores(int top);
        public highScoreModel getHighScoreModel(highScoreModel highScoreModel);
        public bool saveChanges();
    }
}
