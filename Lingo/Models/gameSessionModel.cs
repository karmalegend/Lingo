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
        public int Guesses { get; set; }
        public DateTime lastGuess { get; set; }
        public int Score { get; set; }

        //external relations
        public userModel player { get; set; }
        public string currentword { get; set; }


        public void increaseGuess() {
            this.Guesses++;
        }

        public bool guessedIntime(DateTime newGuess) {
            bool valid = System.Math.Abs((newGuess - lastGuess).TotalSeconds) > 10 ? false : true;
            return valid;
        }

    }
}
