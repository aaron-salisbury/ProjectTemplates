using DotNet.Data.Base.Extensions;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;

namespace DotNet.Data;

public class WebRequester
{
    internal const string COMPRESSION_CLIENT_NAME = "CompressionClient";

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger _logger;

    public WebRequester(IHttpClientFactory httpClientFactory, ILogger logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<string?> SendHTTPJsonRequestAsync(string url, bool useCompression = false, string? jsonContent = null, Dictionary<string, string>? requestHeaders = null, string httpMethod = "GET", string? userAgent = null)
    {
        try
        {
            using (HttpClient client = (useCompression ? _httpClientFactory.CreateClient(COMPRESSION_CLIENT_NAME) : _httpClientFactory.CreateClient()))
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                if (userAgent != null)
                {
                    client.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);
                }

                if (requestHeaders != null)
                {
                    foreach (KeyValuePair<string, string> valueByName in requestHeaders)
                    {
                        client.DefaultRequestHeaders.TryAddWithoutValidation(valueByName.Key, valueByName.Value);
                    }
                }

                if (string.IsNullOrEmpty(jsonContent) && string.Equals(httpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                {
                    string response = await client.GetStringAsync(url);
                    return response;
                }

                using (HttpRequestMessage request = new(new HttpMethod(httpMethod), url))
                {
                    if (!string.IsNullOrEmpty(jsonContent))
                    {
                        request.Content = new StringContent(jsonContent);
                        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    }

                    HttpResponseMessage httpResponse = await client.SendAsync(request);
                    string response = await httpResponse.Content.ReadAsStringAsync();
                    return response;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "HTTP request failed.");
            return null;
        }
    }

    public async Task DownloadFileAsync(string url, string fullFilePath, bool deletePreexisting = false)
    {
        if (deletePreexisting && File.Exists(fullFilePath))
        {
            File.Delete(fullFilePath);
        }

        using (HttpClient client = _httpClientFactory.CreateClient())
        {
            await client.DownloadFileTaskAsync(new Uri(url), fullFilePath);
        }
    }

    public async Task<Stream> GetWebRequestStreamAsync(string url)
    {
        HttpResponseMessage? response = await GetWebRequestResponseAsync(url);

        if (response != null)
        {
            return await response.Content.ReadAsStreamAsync();

        }

        return new MemoryStream();
    }

    public async Task<byte[]> GetWebRequestSerializedAsync(string url)
    {
        HttpResponseMessage? response = await GetWebRequestResponseAsync(url);

        if (response != null)
        {
            return await response.Content.ReadAsByteArrayAsync();
        }

        return [];

    }

    private async Task<HttpResponseMessage?> GetWebRequestResponseAsync(string url)
    {
        try
        {
            using (HttpClient client = _httpClientFactory.CreateClient())
            {
                return await client.GetAsync(url);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Web request failed.");
            return null;
        }
    }
}
