namespace GPTDocs.API.Integrations.MSLearn.Models;

internal class MSLearnSearchResponse
{
    public bool ScopeRemoved { get; set; } = false;
    public int Count { get; set; }
    public string NextLink { get; set; } = null!;
    public string SrchEng { get; set; } = null!;
    public bool TermHasSynonyms { get; set; }

    public IEnumerable<MSLearnFacet> Facets { get; set; }
        = Enumerable.Empty<MSLearnFacet>();

    public IEnumerable<MSLearnResult> Results { get; set; }
        = Enumerable.Empty<MSLearnResult>();
}