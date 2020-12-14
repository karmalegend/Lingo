using System.ComponentModel.DataAnnotations;

namespace Lingo.Models
{
    public class SixLetterWordModel
    {
        public long Id { get; set; }
        [MaxLength(6)]
        public string Word { get; set; }
    }
}
