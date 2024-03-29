using System.ComponentModel.DataAnnotations;

namespace ClientPro.Application.Dtos.Client
{
    public record ClientDto
    {
        public int Id { get; set; }
        public string? ClientName { get; set; }
        public string? Email { get; set; }
    }

    public record CreateClientDto
    {
        [Required]
        [StringLength(300)]
        public string? ClientName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
    }

    public record UpdateClientDto
    {
        [Required]
        [StringLength(300)]
        public string? ClientName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
    }
}
