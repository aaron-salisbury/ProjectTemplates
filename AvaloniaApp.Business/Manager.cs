using AvaloniaApp.Business.DTOs;
using AvaloniaApp.Data;

namespace AvaloniaApp.Business
{
    public static class Manager
    {
        public static string? AppVersion { get; set; }

        public static IHttpClientFactory? HttpClientFactory { get; set; }

        public static string GetCurrentUserStorageDirectory()
        {
            return CRUD.GetCurrentUserStorageDirectory();
        }

        public static async Task UpdateUserStorageDirectoryAsync(string newUserStorageDirectory)
        {
            await CRUD.UpdateUserStorageDirectoryAsync<AppData>(newUserStorageDirectory);
        }
    }
}
