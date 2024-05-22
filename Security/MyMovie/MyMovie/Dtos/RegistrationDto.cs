using System.ComponentModel.DataAnnotations;

namespace MyMovie.Dtos
{
    public class RegistrationDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
