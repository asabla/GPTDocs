namespace GPTDocs.API.Integrations.MSLearn.Models;

internal class Facet
{
    public IEnumerable<Product> Products { get; set; }
        = Enumerable.Empty<Product>();

    // TODO: Implement tags

    public IEnumerable<CategoryValue> Category { get; set; }
        = Enumerable.Empty<CategoryValue>();

    public class CategoryValue
    {
        public int Count { get; set; }
        public string Value { get; set; } = null!;
    }

    public class Product
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