using Lingo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo.Data.Interfaces
{
    public interface IWordsRepo
    {
        public fiveLetterWordModel GetFiveLetterWord();
        public sixLetterWordModel GetSixLetterWord();
        public sevenLetterWordModel GetSevenLetterWord();
    }
}
