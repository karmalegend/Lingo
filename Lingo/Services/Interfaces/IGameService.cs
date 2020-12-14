using Lingo.Models;
using System.Collections.Generic;

namespace Lingo.Services.Interfaces
{
    public interface IGameService
    {
        public GameSessionModel CreateNewGameForUser(string Username);
        public GameSessionModel RetrieveGameSessionModelByUsername(string username);
        public List<List<char>> AttemptGuess(GameSessionModel currentgame, string guessWord);
        public bool GameOver(GameSessionModel currentgame);
        public bool InTime(GameSessionModel currentgame);
        public bool CorrectGuess(List<char> results);
        public bool IncrementGuessCounter(GameSessionModel currentgame);
        public int GetNewWordForGame(GameSessionModel currentgame);
        public bool MatchingWordLengths(GameSessionModel currentgame, string guessWord);
    }
}
