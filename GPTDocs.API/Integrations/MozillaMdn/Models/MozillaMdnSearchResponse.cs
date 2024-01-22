namespace GPTDocs.API.Integrations.MozillaMdn.Models;

internal class MozillaMdnSearchResponse
{
    public IEnumerable<MozillaMdnDocument> Documents { get; set; }
        = Enumerable.Empty<MozillaMdnDocument>();

    public MozillaMdnMetaData MetaData { get; set; } = null!;

    // TODO: implement suggestions
}