using Book.API.Models;

namespace Book.API.Services;

public class UriService : IUriService
{
    private readonly string _baseUri;
    public UriService(string baseUri)
    {
        _baseUri = baseUri;
    }

    public Uri GetBookUri(string uri)
    {
        throw new NotImplementedException();
    }

    public Uri GetAllBookUri(PaginationQuery query = null)
    {
        throw new NotImplementedException();
    }

}
