namespace GPTDocs.API.Integrations.MozillaMdn.Models;

internal class MozillaMdnSearchRequest
{
    public string Query { get; set; } = null!;
    public string Locale { get; set; } = null!;
}