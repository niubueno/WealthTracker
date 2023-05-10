using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WealthTracker.Api
{
    public class YahooFinanceApi : Controller
    {
        private readonly HttpClient _httpClient;

        public YahooFinanceApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal> GetStockPrice(string symbol)
        {
            var quote = await GetStockQuote(symbol);
            return quote.RegularMarketPrice;
        }

        private async Task<YahooFinanceQuote> GetStockQuote(string symbol)
        {
            var url = $"https://query1.finance.yahoo.com/v6/finance/quote?symbols={symbol}";
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync(); 
            var result = JsonConvert.DeserializeObject<YahooFinanceResult>(content);
            return result.QuoteResponse.Result.FirstOrDefault();
        }

        private class YahooFinanceResult
        {
            public YahooFinanceQuoteResponse QuoteResponse { get; set; }
        }

        private class YahooFinanceQuoteResponse
        {
            public List<YahooFinanceQuote> Result { get; set; }
        }

        public class YahooFinanceQuote
        {
            [JsonProperty("regularMarketPrice")]
            public decimal RegularMarketPrice { get; set; }
        }
    }
}
