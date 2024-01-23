namespace GPTDocs.API.Integrations.MSLearn.Models;

internal class MSLearnResult
{
    public string Title { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime LastUpdatedDate { get; set; }
    public string Category { get; set; } = null!;

    // TODO: Implement breadcrumbs

    public MSLearnDisplayUrl DisplayUrl { get; set; } = null!;
    public IEnumerable<MSLearnDescription> Descriptions { get; set; }
        = Enumerable.Empty<MSLearnDescription>();

    public class MSLearnDisplayUrl
    {
        public string Content { get; set; } = null!;
        public IEnumerable<MSLearnHighLight> HitHighlights { get; set; }
            = Enumerable.Empty<MSLearnHighLight>();
    }

    public class MSLearnDescription
    {
        public string Content { get; set; } = null!;
        public IEnumerable<MSLearnHighLight> HitHighlights { get; set; }
            = Enumerable.Empty<MSLearnHighLight>();
    }

    public class MSLearnHighLight
    {
        public int Start { get; set; }
        public int Length { get; set; }
    }
}