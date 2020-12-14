using Lingo.Models;
using System.Collections.Generic;

namespace Lingo.Services.Interfaces
{
    public interface IHighScoreService
    {
        public bool AddNewHighScore(HighScoreModel highScore);
        public List<HighScoreModel> GetHighScores(int top);
    }
}
