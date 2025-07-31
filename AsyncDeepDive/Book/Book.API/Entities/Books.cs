using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book.API.Entities;

public class Books
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }   

    [Required]
    public string Description { get; set; }

    [ForeignKey(nameof(Author))]
    public int AuthorId { get; set; }
    public Author Author { get; set; }
}
