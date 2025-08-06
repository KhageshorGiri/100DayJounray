namespace Book.API.Models;

public class LinkDto
{
    public string? Href { get; private set; }
    public string? Rel {  get; private set; }   
    public string? Method { get; private set; }

    public LinkDto(string herf, string rel, string method)
    {
        Href = herf;
        Rel = rel;
        Method = method;
    }
}
