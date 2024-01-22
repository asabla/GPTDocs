using System.Web;

using GPTDocs.API.Integrations.MSLearn.Models;

namespace GPTDocs.API.Integrations.MSLearn;

internal interface IMSLearnSearchService
{
    Task<MSLearnSearchResponse> SearchAsync(
        string query,
        string scope,
        string locale,
        string[] facets,
        string filter,
        int take = 10,
        bool expandScope = true,
        string partnerId = "LearnSite",
        CancellationToken cancellationToken = default);

    Task<MSLearnSearchResponse> SearchAsync(
        Action<MSLearnSearchRequest> searchRequestAction,
        CancellationToken cancellationToken = default);

    Task<MSLearnSearchResponse> SearchAsync(
        MSLearnSearchRequest searchRequest,
        CancellationToken cancellationToken = default);
}

internal class MSLearnSearchService(
        ILogger<MSLearnSearchService> logger,
        IHttpClientFactory httpClientFactory)
    : IMSLearnSearchService
{
    public async Task<MSLearnSearchResponse> SearchAsync(
            string query,
            string scope,
            string locale,
            string[] facets,
            string filter,
            int take = 10,
            bool expandScope = true,
            string partnerId = "LearnSite",
            CancellationToken cancellationToken = default)
        => await SearchAsync(searchRequest: new()
        {
            Query = query,
            Scope = scope,
            Locale = locale,
            Facets = facets,
            Filter = filter,
            Take = take,
            ExpandScope = expandScope,
            PartnerId = partnerId
        }, cancellationToken: cancellationToken);

    public async Task<MSLearnSearchResponse> SearchAsync(
        Action<MSLearnSearchRequest> searchRequestAction,
        CancellationToken cancellationToken = default)
    {
        MSLearnSearchRequest searchRequest = new();
        searchRequestAction(searchRequest);

        return await SearchAsync(
            searchRequest: searchRequest,
            cancellationToken: cancellationToken);
    }

    public async Task<MSLearnSearchResponse> SearchAsync(
        MSLearnSearchRequest searchRequest,
        CancellationToken cancellationToken = default)
    {
        var httpClient = httpClientFactory.CreateClient(
                name: nameof(MSLearnSearchService));

        var queryString = HttpUtility.ParseQueryString(string.Empty);
        queryString["locale"] = searchRequest.Locale;

        // When using terms it will not need any other parameters
        if (string.IsNullOrWhiteSpace(searchRequest.Terms) is false)
        {
            queryString["terms"] = searchRequest.Terms;
        }
        else
        {
            queryString["search"] = searchRequest.Query;
            queryString["scope"] = searchRequest.Scope;

            // TODO: implement facets

            queryString["$filter"] = searchRequest.Filter;
            queryString["take"] = searchRequest.Take.ToString();

            queryString["expandScope"] = searchRequest.ExpandScope.ToString();
            queryString["partnerId"] = searchRequest.PartnerId;
        }

        HttpResponseMessage response = await httpClient.GetAsync(
            requestUri: $"api/search?{queryString}",
            cancellationToken: cancellationToken);

        if (response.IsSuccessStatusCode is false)
        {
            string errMessage = await response.Content.ReadAsStringAsync();
            logger.LogError(
                message: "Unable to search MSLearn: ({response.StatusCode}) {errMessage}",
                response.StatusCode, errMessage);
        }

        return await response.Content.ReadFromJsonAsync<MSLearnSearchResponse>(cancellationToken) ?? null!;
    }
}