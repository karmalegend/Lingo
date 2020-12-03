using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo.DTO
{
    public class gameSessionDtoRead
    {
        public int Guesses { get; set; }
        public DateTime lastGuess { get; set; }
        public int Score { get; set; }

    }
}
