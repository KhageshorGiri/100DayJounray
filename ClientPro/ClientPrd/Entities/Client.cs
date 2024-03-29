using System.ComponentModel.DataAnnotations;

namespace ClientPro.Domain.Entities
{
    public class Client : BaseEntity
    {
        [Required]
        [StringLength(300)]
        public string? ClientName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
