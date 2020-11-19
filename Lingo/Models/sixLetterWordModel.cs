using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo.Models
{
    public class sixLetterWordModel : Word
    {
        public long Id { get; set; }
        [MaxLength(6)]
        public string word { get; set; }

        public Dictionary<char, char> guessAttempt(string word)
        {
            throw new NotImplementedException();
        }
    }
}
