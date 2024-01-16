using System.Net.Http.Headers;

using GPTDocs.API.Integrations.MSLearn.Models;

using Microsoft.AspNetCore.Mvc;

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

    public static RouteGroupBuilder? ConfigureMSLearnEndpoints(
        this RouteGroupBuilder? builder)
    {
        if (builder is null)
            return null!;

        var msLearnGroup = builder.MapGroup("/microsoftlearn");

        msLearnGroup.MapGet("/", async (
            [FromServices] IMSLearnSearchService msLearnSearchService,
            [FromQuery] string searchQuery = "") =>
                await msLearnSearchService.SearchAsync(new SearchRequest
                {
                    Query = searchQuery,
                    Scope = ".Net",
                    Locale = "en-us",
                    Facets = [ "category", "products", "tags" ],
                    Filter = "(scopes/any(s: s eq '.Net'))",
                    Take = 10,
                    ExpandScope = true,
                    PartnerId = "LearnSite"
                }) is SearchResponse searchResponse
                    ? Results.Ok(searchResponse)
                    : Results.NotFound())
            .Produces<SearchResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithName("MSLearnSearch")
            .WithOpenApi();

        return builder;
    }
}