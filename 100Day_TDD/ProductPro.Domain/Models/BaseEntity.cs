using System.ComponentModel.DataAnnotations;

namespace ProductPro.Domain.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
