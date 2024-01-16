namespace GPTDocs.API.Integrations.MSLearn.Models;

internal class Result
{
    public string Title { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime LastUpdatedDate { get; set; }
    public string Category { get; set; } = null!;

    // TODO: Implement breadcrumbs

    public DisplayUrlValue DisplayUrl { get; set; } = null!;
    public IEnumerable<DescriptionValue> Descriptions { get; set; }
        = Enumerable.Empty<DescriptionValue>();

    public class DisplayUrlValue
    {
        public string Content { get; set; } = null!;
        public IEnumerable<HighLight> HitHighlights { get; set; }
            = Enumerable.Empty<HighLight>();
    }

    public class DescriptionValue
    {
        public string Content { get; set; } = null!;
        public IEnumerable<HighLight> HitHighlights { get; set; }
            = Enumerable.Empty<HighLight>();
    }

    public class HighLight
    {
        public int Start { get; set; }
        public int Length { get; set; }
    }
}