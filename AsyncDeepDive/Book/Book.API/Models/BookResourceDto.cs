using System.ComponentModel.DataAnnotations;

namespace Book.API.Models;

public class BookResourceDto : PaginationQuery
{
    public string OrderBy { get; set; } = "Title";
}

public class BookDto
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }
}