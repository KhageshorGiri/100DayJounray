using System.ComponentModel.DataAnnotations;

namespace ProductPro.Domain.Models
{
    public class Product : BaseEntity
    {
        [Required]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        public string ProductDescription { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }
    }
}
