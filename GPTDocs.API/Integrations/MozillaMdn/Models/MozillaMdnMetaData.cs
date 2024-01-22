namespace GPTDocs.API.Integrations.MozillaMdn.Models;

internal class MozillaMdnMetaData
{
    public uint Took_Ms { get; set; } = 0;
    public uint Size { get; set; } = 0;
    public uint Page { get; set; } = 0;

    public TotalObject Total { get; set; } = null!;

    internal class TotalObject
    {
        public uint Value { get; set; } = 0;
        public string Relation { get; set; } = null!;
    }
}