namespace Book.API.Models;

public class PaginationQuery
{
    public PaginationQuery()
    {
        PageNumber = 1;
        PageSize = 10;
    }

    public PaginationQuery(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
