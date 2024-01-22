using LondonStockAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LondonStockAPI.Controllers
{
    public class StockController
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        public async Task ShowSingularStockInfo()
        {
            do
            {
                Console.Write("Enter stock symbol: ");
            string symbol = Console.ReadLine();

            Console.Write("Enter date (YYYY-MM-DD): ");
            string date = Console.ReadLine();

            var stockInfoResult = await _stockService.GetStockInfoAsync(symbol, date);

            if (stockInfoResult.StockInfo != null)
            {
                // Print specific information from StockInfo
                Console.WriteLine($"Symbol: {stockInfoResult.StockInfo.Symbol}");
                Console.WriteLine($"Open: {stockInfoResult.StockInfo.Open}");
                Console.WriteLine($"High: {stockInfoResult.StockInfo.High}");
                Console.WriteLine($"Low: {stockInfoResult.StockInfo.Low}");
                Console.WriteLine($"Close: {stockInfoResult.StockInfo.Close}");
                Console.WriteLine($"Volume: {stockInfoResult.StockInfo.Volume}");
                Console.WriteLine($"AfterHours: {stockInfoResult.StockInfo.AfterHours}");
                Console.WriteLine($"PreMarket: {stockInfoResult.StockInfo.PreMarket}");
            }
            else
            {
                // Print the error message
                Console.WriteLine($"Failed to retrieve stock information. Reason: {stockInfoResult.ErrorMessage}");
            }

                Console.Write("Do you want to search another stock? (yes/no): ");
            } while (Console.ReadLine()?.ToLower() == "yes");
        }
    }

}
