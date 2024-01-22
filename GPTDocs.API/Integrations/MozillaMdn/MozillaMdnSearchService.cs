using System.Web;

using GPTDocs.API.Integrations.MozillaMdn.Models;

namespace GPTDocs.API.Integrations.MozillaMdn;

internal interface IMozillaMdnSearchService 
{
    Task<MozillaMdnSearchResponse> SearchAsync(
        string query,
        string locale = "en-US",
        CancellationToken cancellationToken = default);

    Task<MozillaMdnSearchResponse> SearchAsync(
        Action<MozillaMdnSearchRequest> searchRequestAction,
        CancellationToken cancellationToken = default);

    Task<MozillaMdnSearchResponse> SearchAsync(
        MozillaMdnSearchRequest searchRequest,
        CancellationToken cancellationToken = default);
}

internal class MozillaMdnSearchService(
        ILogger<MozillaMdnSearchService> logger,
        IHttpClientFactory httpClientFactory)
    : IMozillaMdnSearchService
{
    public async Task<MozillaMdnSearchResponse> SearchAsync(
            string query,
            string locale = "en-US",
            CancellationToken cancellationToken = default)
        => await SearchAsync(searchRequest: new()
        {
            Query = query,
            Locale = locale,
        }, cancellationToken: cancellationToken);

    public async Task<MozillaMdnSearchResponse> SearchAsync(
        Action<MozillaMdnSearchRequest> searchRequestAction,
        CancellationToken cancellationToken = default)
    {
        MozillaMdnSearchRequest searchRequest = new();
        searchRequestAction(searchRequest);

        return await SearchAsync(
            searchRequest: searchRequest,
            cancellationToken: cancellationToken);
    }

    public async Task<MozillaMdnSearchResponse> SearchAsync(
        MozillaMdnSearchRequest searchRequest,
        CancellationToken cancellationToken = default)
    {
        var httpClient = httpClientFactory.CreateClient(
                name: nameof(MozillaMdnSearchService));

        var queryString = HttpUtility.ParseQueryString(string.Empty);
        queryString["q"] = searchRequest.Query;
        queryString["locale"] = searchRequest.Locale;

        HttpResponseMessage response = await httpClient.GetAsync(
            requestUri: $"api/v1/search?{queryString}",
            cancellationToken: cancellationToken);

        if (response.IsSuccessStatusCode is false)
        {
            string errMessage = await response.Content.ReadAsStringAsync();
            logger.LogError(
                message: "Unable to search MSLearn: ({response.StatusCode}) {errMessage}",
                response.StatusCode, errMessage);
        }

        return await response.Content.ReadFromJsonAsync<MozillaMdnSearchResponse>(cancellationToken) ?? null!;
    }
}