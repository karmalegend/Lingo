using Lingo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo.Services.Interfaces
{
    public interface IHighScoreService
    {
        public bool addNewHighScore(highScoreModel highScore);
        public List<highScoreModel> getHighScores(int top);
    }
}
