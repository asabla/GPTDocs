using GPTDocs.API.Integrations.MSLearn.Models;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GPTDocs.API.Integrations.MSLearn;

internal static class MSLearnEndpoints
{
    public static IEndpointRouteBuilder MapMSLearnEndpoints(
        this IEndpointRouteBuilder builder)
    {
        var msLearnGroup = builder.MapGroup("/microsoftlearn");
        msLearnGroup
            .WithName("Microsoft Learn")
            .WithSummary("API endpoints for Microsoft Learn");

        msLearnGroup
            .MapGet("/", async (
                [FromServices] IMSLearnSearchService msLearnSearchService,
                [FromQuery] string searchQuery = "") =>
                    await SearchAsync(msLearnSearchService, searchQuery))
            .Produces<MSLearnSearchResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("Micorsoft Learn Search")
            .WithDescription("This endpoint will search all categories on Microsoft Learn without any filters")
            .WithSummary("Search everything on Microsoft Learn")
            .WithOpenApi();

        msLearnGroup.MapGet("/azure", async (
                [FromServices] IMSLearnSearchService msLearnSearchService,
                [FromQuery] string searchQuery = "") =>
                    await SearchAzureAsync(msLearnSearchService, searchQuery))
            .Produces<MSLearnSearchResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithName("Azure Search")
            .WithDescription("This endpoint will search all categories on Microsoft Learn with filters for Azure content")
            .WithSummary("Search Microsoft Learn with filters for Azure content")
            .WithOpenApi();

        msLearnGroup.MapGet("/dotnet", async (
                [FromServices] IMSLearnSearchService msLearnSearchService,
                [FromQuery] string searchQuery = "") =>
                    await SearchDotNetAsync(msLearnSearchService, searchQuery))
            .Produces<MSLearnSearchResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithName(".Net Search")
            .WithDescription("This endpoint will search all categories on Microsoft Learn with filters for .Net content")
            .WithSummary("Search Microsoft Learn with filters for .Net content")
            .WithOpenApi();

        return builder;
    }

    private static async Task<Results<Ok<MSLearnSearchResponse>, NotFound>> SearchAsync(
        IMSLearnSearchService msLearnSearchService,
        string searchQuery = "") =>
            await msLearnSearchService.SearchAsync(new MSLearnSearchRequest
            {
                Terms = searchQuery,
                Locale = "en-us",
                Take = 10,
            }) is MSLearnSearchResponse searchResponse
                ? TypedResults.Ok(searchResponse)
                : TypedResults.NotFound();

    private static async Task<Results<Ok<MSLearnSearchResponse>, NotFound>> SearchAzureAsync(
        IMSLearnSearchService msLearnSearchService,
        string searchQuery) =>
            await msLearnSearchService.SearchAsync(new MSLearnSearchRequest
            {
                Query = searchQuery,
                Scope = "Azure",
                Locale = "en-us",
                Facets = ["category", "products", "tags"],
                Filter = "(scopes/any(s: s eq 'Azure'))",
                Take = 10,
                ExpandScope = true,
                PartnerId = "LearnSite"
            }) is MSLearnSearchResponse searchResponse
                ? TypedResults.Ok(searchResponse)
                : TypedResults.NotFound();

    private static async Task<Results<Ok<MSLearnSearchResponse>, NotFound>> SearchDotNetAsync(
        IMSLearnSearchService msLearnSearchService,
        string searchQuery) =>
            await msLearnSearchService.SearchAsync(new MSLearnSearchRequest
            {
                Query = searchQuery,
                Scope = ".Net",
                Locale = "en-us",
                Facets = ["category", "products", "tags"],
                Filter = "(scopes/any(s: s eq '.Net'))",
                Take = 10,
                ExpandScope = true,
                PartnerId = "LearnSite"
            }) is MSLearnSearchResponse searchResponse
                ? TypedResults.Ok(searchResponse)
                : TypedResults.NotFound();
}