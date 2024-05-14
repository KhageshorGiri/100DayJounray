using System.ComponentModel.DataAnnotations;

namespace TODOgRpcService.Models
{
    public class ToDoItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? ItemName { get; set; }

        [Required]
        public string? Description { get; set; }

        public string Status { get; set; } = "NEW";
    }
}
