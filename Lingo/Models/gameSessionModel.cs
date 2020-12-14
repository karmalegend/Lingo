using System;

namespace Lingo.Models
{
    public class GameSessionModel
    {
        public long Id { get; set; }
        public int Guesses { get; set; }
        public DateTime LastGuess { get; set; }
        public int Score { get; set; }

        //external relations
        public UserModel Player { get; set; }
        public string Currentword { get; set; }


        public void IncreaseGuess() {
            this.Guesses++;
        }

        public bool GuessedIntime(DateTime newGuess) {
            bool valid = !(System.Math.Abs((newGuess - LastGuess).TotalSeconds) > 10);
            return valid;
        }

    }
}
