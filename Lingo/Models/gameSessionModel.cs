using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo.Models
{
    public class gameSessionModel
    {
        public long Id { get; set; }
        public int? Guesses { get; set; }
        public DateTime? lastGuess { get; set; }
        public int? Score { get; set; }

        //external relations
        public userModel player { get; set; }
        public string currentword { get; set; }

        [NotMapped]
        public Word _currentWord { get; set; }

    }
}
