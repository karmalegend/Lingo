using Lingo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo.Services.Interfaces
{
    public interface IGameService
    {
        public gameSessionModel createNewGameForUser(string Username);
        public gameSessionModel retrieveGameSessionModelByUsername(string username);
        public List<List<char>> attemptGuess(gameSessionModel currentgame, string guessWord);
        public bool gameOver(gameSessionModel currentgame);
        public bool inTime(gameSessionModel currentgame);
        public bool correctGuess(List<char> results);
        public bool incrementGuessCounter(gameSessionModel currentgame);
        public int getNewWordForGame(gameSessionModel currentgame);
        public bool matchingWordLengths(gameSessionModel currentgame, string guessWord);
    }
}
