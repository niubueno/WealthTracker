using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WealthTracker.Api;
using WealthTracker.Models;

namespace WealthTracker.Controllers
{
    public class StockDataController : Controller
    {
        private readonly HttpClient _httpClient;

        public StockDataController()
        {
            _httpClient = new HttpClient();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

    }
}
