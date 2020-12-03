using Lingo.Data.Interfaces;
using Lingo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo.Data.Repository
{
    public class WordsRepository : IWordsRepo
    {
        private readonly LingoContext _context;

        public WordsRepository(LingoContext lingoContext) {
            _context = lingoContext;
        }


        //https://stackoverflow.com/questions/7781893/ef-code-first-how-to-get-random-rows

        public fiveLetterWordModel GetFiveLetterWord()
        {
            return _context.fiveLetterWords.OrderBy(o => Guid.NewGuid()).First();
        }

        public sevenLetterWordModel GetSevenLetterWord()
        {
            return _context.sevenLetterWords.OrderBy(o => Guid.NewGuid()).First();
        }

        public sixLetterWordModel GetSixLetterWord()
        {
            return _context.sixLetterWords.OrderBy(o => Guid.NewGuid()).First();
        }
    }
}
