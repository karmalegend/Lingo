using System.ComponentModel.DataAnnotations;

namespace Lingo.Models
{
    public class SevenLetterWordModel
    {
        public long Id { get; set; }
        [MaxLength(7)]
        public string Word { get; set; }
    }
}
