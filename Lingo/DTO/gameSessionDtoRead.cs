using System;

namespace Lingo.DTO
{
    public class GameSessionDtoRead
    {
        public int Guesses { get; set; }
        public DateTime LastGuess { get; set; }
        public int Score { get; set; }

    }
}
