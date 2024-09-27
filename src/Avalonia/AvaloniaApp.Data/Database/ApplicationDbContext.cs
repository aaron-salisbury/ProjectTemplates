using AvaloniaApp.Data.Base;
using AvaloniaApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaApp.Data.Database;

public class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        string appFolderPath = IO.GetAppDirectoryPath();
        string appName = new DirectoryInfo(appFolderPath).Name;
        string dbPath = Path.Join(appFolderPath, $"{appName}.db");

        options.UseSqlite($"Data Source={dbPath}");
    }

    internal DbSet<EndUser> EndUsers { get; set; }
    internal DbSet<UserCredential> UserCredentials { get; set; }
    internal DbSet<UserConfig> UserConfigs { get; set; }
}
