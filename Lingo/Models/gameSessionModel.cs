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

        private Dictionary<char, char> checkResult(string wordToGuess, string guessWord)
        {
            Dictionary<char, char> results = new Dictionary<char, char>();

            for (int i = 0; i < wordToGuess.Length; i++)
            {
                char current = guessWord[i];

                //if letter is guessed correctly
                if (wordToGuess[i].Equals(current))
                {
                    results.Add(current, 'C');
                }
                // if letter is correct but at the wrong spot.
                else if (wordToGuess.Contains(current))
                {
                    results.Add(current, 'P');
                }
                // if the letter is plain wrong.
                else
                {
                    results.Add(current, 'A');
                }
            }
            return results;

        }

    }
}
