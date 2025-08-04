namespace Book.API.Models;

public class PaginatedApiResponse<T>
{
    public PaginatedApiResponse() { }
    
    public PaginatedApiResponse(T data) 
    {
        Data = data;
    }


    public T Data { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string PreviousPage { get; set; }
    public string NextPage { get; set; }
}
