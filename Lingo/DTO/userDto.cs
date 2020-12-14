using System.ComponentModel.DataAnnotations;

namespace Lingo.DTO
{
    public class UserDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}
