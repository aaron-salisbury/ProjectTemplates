using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AvaloniaApp.Data.Entities;

[Index(nameof(EndUserId))]
public class UserConfig
{
    [Key]
    public int UserConfigId { get; set; }

    [Required, ForeignKey(nameof(EndUser))]
    public int EndUserId { get; set; }

    public EndUser? EndUser { get; set; }

    public string? Email { get; set; }

    public int? MarketDataAPI { get; set; }

    public string? MarketDataAPIKey { get; set; }

    public int? ExchangeAPI { get; set; }

    public string? ExchangeAPIKey { get; set; }

    public string? ExchangeSecretKey { get; set; }
}
