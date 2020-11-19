using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo.Models
{
    public class sevenLetterWordModel : Word
    {
        public long Id { get; set; }
        public string Word { get; set; }

        public Dictionary<char, char> guessAttempt(string word)
        {
            throw new NotImplementedException();
        }
    }
}
