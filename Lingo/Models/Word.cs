using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo.Models
{
    public interface Word
    {
        public Dictionary<char, char> guessAttempt(String word);
    }
}
