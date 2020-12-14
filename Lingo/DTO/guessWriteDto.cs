using System.ComponentModel.DataAnnotations;

namespace Lingo.DTO
{
    public class GuessWriteDto
    {
        [Required]
        public string Guess { get; set; }
    }
}
