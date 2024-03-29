using System.ComponentModel.DataAnnotations;

namespace ClientPro.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
       
        [Required]
        public int CreatedBy { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }

        public int? ModifiedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedDate { get; set; }
    }
}
