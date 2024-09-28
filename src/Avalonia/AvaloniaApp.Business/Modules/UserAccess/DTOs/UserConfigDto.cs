using System;

namespace DotNet.Business.Modules.UserAccess.DTOs
{
    public record UserConfigDto
    {
        public enum MarketDataAPIs
        {
            CoinAPI
        }

        public enum ExchangeAPIs
        {
            Binance

        }

        public int UserConfigId { get; init; }

        public int EndUserId { get; init; }

        public string? Email { get; set; }

        public MarketDataAPIs? MarketDataAPI { get; set; }

        public string? MarketDataAPIKey { get; set; }

        public ExchangeAPIs? ExchangeAPI { get; set; }

        public string? ExchangeAPIKey { get; set; }

        public string? ExchangeSecretKey { get; set; }
    }
}
