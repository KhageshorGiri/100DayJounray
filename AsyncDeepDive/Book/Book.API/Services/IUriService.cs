using Book.API.Models;

namespace Book.API.Services;

public interface IUriService
{
    Uri GetBookUri(string uri);
    Uri GetAllBookUri(PaginationQuery query = null);
}
