using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LondonStockAPI.Models
{
    public class StockServiceResult
    {
        public StockInfo StockInfo { get; set; }
        public string ErrorMessage { get; set; }
    }
}
