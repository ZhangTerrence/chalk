using System.Net.Http.Json;
using System.Text.Json;

namespace chalk.IntegrationTests.Helpers;

public static class HttpClientExtensions
{
    private static readonly JsonSerializerOptions Options = new() { IncludeFields = true };

    public static async Task<(HttpResponseMessage, TData?)> GetAsync<TData>(
        this HttpClient httpClient,
        string url,
        ITestOutputHelper? logger
    )
    {
        try
        {
            var response = await httpClient.GetAsync(url);
            var data = await response.Content.ReadFromJsonAsync<TData>(Options);
            return (response, data);
        }
        catch (Exception e)
        {
            logger?.WriteLine(e.Message);
            throw;
        }
    }

    public static async Task<(HttpResponseMessage, TData?)> PostAsync<TData, TBody>(
        this HttpClient httpClient,
        string url,
        TBody body,
        ITestOutputHelper? logger
    )
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync(url, body);
            var data = await response.Content.ReadFromJsonAsync<TData>(Options);
            return (response, data);
        }
        catch (Exception e)
        {
            logger?.WriteLine(e.Message);
            throw;
        }
    }
}