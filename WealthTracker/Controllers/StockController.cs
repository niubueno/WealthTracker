using Microsoft.AspNetCore.Mvc;
using Amazon.DynamoDBv2.DataModel;
using WealthTracker.Models;

namespace WealthTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IDynamoDBContext _context;
        public StockController(IDynamoDBContext context)
        {
            _context = context;
        }

        [HttpGet("{stockId}")]
        public async Task<IActionResult> GetById(string stockId)
        {
            var stock = await _context.LoadAsync<Stock>(stockId);
            if (stock == null) return NotFound();
            return Ok(stock);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStocks()
        {
            var stocks = await _context.ScanAsync<Stock>(default).GetRemainingAsync();
            return Ok(stocks);
        }


        [HttpPost]
        public async Task<IActionResult> CreateStock(Stock stockRequest)
        {
            var stock = await _context.LoadAsync<Stock>(stockRequest.Id);
            if (stock != null) return BadRequest($"stock with Id {stockRequest.Id} Already Exists");
            await _context.SaveAsync(stockRequest);
            return Ok(stockRequest);
        }

        [HttpDelete("{stockId}")]
        public async Task<IActionResult> Deletestock(int stockId)
        {
            var stock = await _context.LoadAsync<Stock>(stockId);
            if (stock == null) return NotFound();
            await _context.DeleteAsync(stock);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Updatestock(Stock stockRequest)
        {
            var stock = await _context.LoadAsync<Stock>(stockRequest.Id);
            if (stock == null) return NotFound();
            await _context.SaveAsync(stockRequest);
            return Ok(stockRequest);
        }

    }
}
