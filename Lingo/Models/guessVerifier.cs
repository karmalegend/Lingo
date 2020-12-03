using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo.Models
{
    public class guessVerifier
    {
        /// <summary>
        /// Takes the user guess and compares it to the correct word,
        /// goes over all the correct letters before moving onto the  possibility of 
        /// both present and absent ones.
        /// 
        /// Uses an List builder that fills a list with x's to match a letter and result index within the array.
        /// This allows for the insert at index method.
        /// </summary>
        /// <param name="wordToGuess"></param>
        /// <param name="guessWord"></param>
        /// <returns>List containing a list of letters and a list of results.</returns>
        public static List<List<char>> checkResult(string wordToGuess, string guessWord)
        {
            //fill array based on the current max word length. this so we can use insert(index,object).
            //sadly C# doesnt provide a replace method.
            List<char> letters = generateFillLists(wordToGuess);
            List<char> results = generateFillLists(wordToGuess);

            List<char> judged = new List<char>();

            //first mark the correct letters.
            for (int i = 0; i < wordToGuess.Length; i++)
            {
                char current = guessWord[i];

                //if letter is guessed correctly
                if (wordToGuess[i].Equals(current))
                {
                    judged.Add(current);
                    letters = replaceByIndex(i, current, letters);
                    results = replaceByIndex(i, 'C', results);
                }
            }

            //now move onto the present and absent
            for (int i = 0; i < wordToGuess.Length; i++)
            {
                char current = guessWord[i];

                //count the appearances of a letter to verify wether we've already checked all present letters
                //or if there's still a possibility of a present / correct.
                int appearances = wordToGuess.Count(c => c == current);

                int judgedAppearances = 0;
                foreach (char character in judged)
                {
                    if (character == current)
                    {
                        judgedAppearances++;
                    }
                }

                
                if (wordToGuess[i].Equals(current))
                {
                    continue;
                }
                // if letter is correct but at the wrong spot.
                else if (wordToGuess.Contains(current) && appearances > judgedAppearances)
                {
                    judged.Add(current);
                    letters = replaceByIndex(i, current, letters);
                    results = replaceByIndex(i, 'P', results);
                }
                // if the letter is plain wrong.
                else
                {
                    letters = replaceByIndex(i, current, letters);
                    results = replaceByIndex(i, 'A', results);
                }
            }

            return parseResults(letters,results);

        }


        /// <summary>
        /// Generate a list with the apprioriate length filled with 'x' to allow for the
        /// insert at index method.
        /// </summary>
        /// <param name="wordToGuess"></param>
        /// <returns>['x'*n]</returns>
        private static List<char> generateFillLists(string wordToGuess) {
            List<char> fillList = new List<char>();
            for (int i = 0; i < wordToGuess.Length; i++) {
                fillList.Add('X');
            }
            return fillList;
        }

        private static List<char> replaceByIndex(int index, char value, List<char> list) {
            list.RemoveAt(index);
            list.Insert(index, value);
            return list;
        }

        private static List<List<char>> parseResults(List<char> letters, List<char> results) {
            return new List<List<char>> {
                letters,results
            };
        }

    }
}
