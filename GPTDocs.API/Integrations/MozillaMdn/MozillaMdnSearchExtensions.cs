using System.Net.Http.Headers;

namespace GPTDocs.API.Integrations.MozillaMdn;

internal static class MozillaMdnSearchExtensions
{
    public static WebApplicationBuilder ConfigureMozillaMdnSearch(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient(
            name: nameof(MozillaMdnSearchService),
            configureClient: client =>
            {
                client.BaseAddress = new Uri("https://developer.mozilla.org");

                // Clear default headers just in case there is junk in there
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            });

        builder.Services.AddTransient<IMozillaMdnSearchService, MozillaMdnSearchService>();

        return builder;
    }
}