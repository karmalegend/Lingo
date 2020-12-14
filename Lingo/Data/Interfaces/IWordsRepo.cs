using Lingo.Models;

namespace Lingo.Data.Interfaces
{
    public interface IWordsRepo
    {
        public FiveLetterWordModel GetFiveLetterWord();
        public SixLetterWordModel GetSixLetterWord();
        public SevenLetterWordModel GetSevenLetterWord();
    }
}
