using LondonStockAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LondonStockAPI.Services
{
    public interface IStockService
    {
        Task<StockServiceResult> GetStockInfoAsync(string symbol, string date);
    }
}
