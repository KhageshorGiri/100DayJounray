namespace Book.API.Models;

public class PaginationQuery
{
    const int _maxPageSize = 20;
    public int PageNumber { get; set; }

    private int _pageSize = 10;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > _maxPageSize) ? _maxPageSize : value;
    }
}
