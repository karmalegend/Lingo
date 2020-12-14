using System.ComponentModel.DataAnnotations;

namespace Lingo.Models
{
    public class FiveLetterWordModel
    {
        public long Id { get; set; }
        [MaxLength(5)]
        public string Word { get; set; }
    }
}
