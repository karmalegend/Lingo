using Lingo.Data.Interfaces;
using Lingo.Models;
using System;
using System.Linq;

namespace Lingo.Data.Repository
{
    public class WordsRepository : IWordsRepo
    {
        private readonly LingoContext _context;

        public WordsRepository(LingoContext lingoContext) {
            _context = lingoContext;
        }


        //https://stackoverflow.com/questions/7781893/ef-code-first-how-to-get-random-rows

        public FiveLetterWordModel GetFiveLetterWord()
        {
            return _context.FiveLetterWords.OrderBy(o => Guid.NewGuid()).First();
        }

        public SevenLetterWordModel GetSevenLetterWord()
        {
            return _context.SevenLetterWords.OrderBy(o => Guid.NewGuid()).First();
        }

        public SixLetterWordModel GetSixLetterWord()
        {
            return _context.SixLetterWords.OrderBy(o => Guid.NewGuid()).First();
        }
    }
}
