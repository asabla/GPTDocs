namespace GPTDocs.API.Integrations.MSLearn.Models;

internal class SearchRequest
{
    public string Terms { get; set; } = null!;
    public string Query { get; set; } = null!;
    public string Scope { get; set; } = null!;
    public string Locale { get; set; } = null!;
    public string[] Facets { get; set; } = null!;
    public string Filter { get; set; } = null!;
    public int Take { get; set; } = 10;
    public bool ExpandScope { get; set; } = true;
    public string PartnerId { get; set; } = "LearnSite";
}