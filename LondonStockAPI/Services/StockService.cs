using LondonStockAPI.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LondonStockAPI.Services
{
    public class StockService : IStockService
    {

        private readonly string _apiKey;

        public StockService(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<StockServiceResult> GetStockInfoAsync(string symbol, string date)
        {
            // Convert symbol to upper case
            symbol = symbol.ToUpper();

            var client = new RestClient($"https://api.polygon.io/v1/open-close/{symbol}/{date}?adjusted=true&apiKey={_apiKey}");
            var request = new RestRequest();
            request.Method = Method.Get;

            var response = await client.ExecuteAsync<StockInfo>(request);

            return ValidateResponse(response, symbol);
        }

        private StockServiceResult ValidateResponse(RestSharp.RestResponse<StockInfo> response, string symbol)
        {
            var result = new StockServiceResult();

            if (response.IsSuccessful)
            {
                result.StockInfo = response.Data;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                result.ErrorMessage = $"Could not find stock that matches ticker {symbol}. Please try again";
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                result.ErrorMessage = "Invalid API key. Please check your API key and try again";
            }
            else
            {
                result.ErrorMessage = $"Failed to retrieve stock information. Status code: {response.StatusCode}";
            }

            return result;
        }
    }
}
