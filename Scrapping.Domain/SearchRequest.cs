namespace Scrapping.Domain;
public class SearchRequest
{
    public string? UserInput { get; set; }
    public string? ContinuationToken { get; set; }
}
