using System;

namespace AvaloniaApp.Data.Base.Extensions;

internal static class HttpClientExtensions
{
    /// <summary>
    /// Downloads the specified resource to a local file as an asynchronous operation.
    /// </summary>
    internal static async Task DownloadFileTaskAsync(this HttpClient client, Uri address, string fileName)
    {
        // ref: https://stackoverflow.com/a/66270371

        using (Stream stream = await client.GetStreamAsync(address))
        using (FileStream fileStream = new(fileName, FileMode.CreateNew))
        {
            await stream.CopyToAsync(fileStream);
        }
    }
}
