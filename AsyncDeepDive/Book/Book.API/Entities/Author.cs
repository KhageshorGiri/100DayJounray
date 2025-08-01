using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Book.API.Entities;

public class Author
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [JsonIgnore]
    public ICollection<Books>? Books { get; set; }
}
