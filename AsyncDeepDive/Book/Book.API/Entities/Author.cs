using System.ComponentModel.DataAnnotations;

namespace Book.API.Entities;

public class Author
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    public ICollection<Books>? Books { get; set; }
}
