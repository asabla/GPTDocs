using System.Net.Http.Headers;

namespace GPTDocs.API.Integrations.MSLearn;

internal static class MSLearnSearchExtensions
{
    public static WebApplicationBuilder ConfigureMSLearnSearch(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient(
            name: nameof(MSLearnSearchService),
            configureClient: client =>
            {
                client.BaseAddress = new Uri("https://learn.microsoft.com/");

                // Clear default headers just in case there is junk in there
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            });

        builder.Services.AddTransient<IMSLearnSearchService, MSLearnSearchService>();

        return builder;
    }
}