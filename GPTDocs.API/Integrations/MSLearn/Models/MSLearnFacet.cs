namespace GPTDocs.API.Integrations.MSLearn.Models;

internal class MSLearnFacet
{
    public IEnumerable<MSLearnProduct> Products { get; set; }
        = Enumerable.Empty<MSLearnProduct>();

    // TODO: Implement tags

    public IEnumerable<MSLearnCategory> Category { get; set; }
        = Enumerable.Empty<MSLearnCategory>();

    public class MSLearnCategory
    {
        public int Count { get; set; }
        public string Value { get; set; } = null!;
    }

    public class MSLearnProduct
    {
        public string DisplayName { get; set; } = null!;
        public int Count { get; set; }
        public string Value { get; set; } = null!;
        public string Type { get; set; } = null!;

        // TODO: look into this issue
        // Causes circular reference, needs a better impelementation
        // public IEnumerable<Product> Children { get; set; }
        //     = Enumerable.Empty<Product>();
    }
}