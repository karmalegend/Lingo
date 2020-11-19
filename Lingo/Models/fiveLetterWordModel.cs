using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo.Models
{
    public class fiveLetterWordModel : Word
    {
        public long Id { get; set; }
        [MaxLength(5)]
        public string word { get; set; }

        public Dictionary<char, char> guessAttempt(string word)
        {
            throw new NotImplementedException();
        }
    }
}
