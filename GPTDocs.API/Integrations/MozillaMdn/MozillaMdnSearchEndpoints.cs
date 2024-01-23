using GPTDocs.API.Integrations.MozillaMdn.Models;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GPTDocs.API.Integrations.MozillaMdn;

internal static class MozillaMdnEndpoints
{
    public static IEndpointRouteBuilder MapMozillaMdnEndpoints(
        this IEndpointRouteBuilder builder)
    {
        var mozillaGroup = builder.MapGroup("/mozillamdn");
        mozillaGroup
            .WithName("Mozilla MDN")
            .WithSummary("API endpoints for Mozilla MDN");

        mozillaGroup
            .MapGet("/", async (
                [FromServices] IMozillaMdnSearchService mozillaMdnSearchService,
                [FromQuery] string searchQuery = "") =>
                    await SearchAsync(mozillaMdnSearchService, searchQuery))
            .Produces<MozillaMdnSearchResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("Mozilla MDN Search")
            .WithDescription("This endpoint searches in in HTML, CSS, Javascript, HTPP, Web APIs, Web Extensions and Web Technology categories")
            .WithSummary("Search all categories on Mozilla MDN without any filters");

        return builder;
    }

    private static async Task<Results<Ok<MozillaMdnSearchResponse>, NotFound>> SearchAsync(
        IMozillaMdnSearchService mozillaMdnSearchService,
        string searchQuery = "") =>
            await mozillaMdnSearchService.SearchAsync(new MozillaMdnSearchRequest
            {
                Query = searchQuery,
                Locale = "en-us",
            }) is MozillaMdnSearchResponse searchResponse
                ? TypedResults.Ok(searchResponse)
                : TypedResults.NotFound();
}