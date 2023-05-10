using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text.Json;
using WealthTracker.Api;
using WealthTracker.Models;

namespace WealthTracker.Controllers
{
    public class StockDataController : Controller
    {
        private readonly HttpClient _httpClient;
        private YahooFinanceApi _yahooFinanceApi;

        public StockDataController()
        {
            _httpClient = new HttpClient();
            _yahooFinanceApi = new YahooFinanceApi(_httpClient);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetStockData([FromForm] Stock stock)
        {
            var price = await _yahooFinanceApi.GetStockPrice(stock.CompanySymbol);
            stock.CurrentPrice = ((float)price);
            ViewBag.Quote = price;

            return View("Index", stock);
        }

    }
}
