namespace GPTDocs.API.Integrations.MozillaMdn.Models;

internal class MozillaMdnDocument
{
    public string Mdn_Url { get; set; } = null!;
    public float Score { get; set; }
    public string Title { get; set; } = null!;
    public string Locale { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public float Popularity { get; set; }
    public string Summary { get; set; } = null!;

    public string Url => $"https://developer.mozilla.org/{Locale}/docs/{Slug}";

    public HighlightObject Highlight { get; set; } = null!;

    internal class HighlightObject
    {
        public IEnumerable<string> Body { get; set; }
            = Enumerable.Empty<string>();

        public IEnumerable<string> Title { get; set; }
            = Enumerable.Empty<string>();
    }
}