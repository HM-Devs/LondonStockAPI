using System;
using System.Threading.Tasks;

public class StockInfo
{
    public string Date { get; set; }
    public string Symbol { get; set; }
    public decimal Open { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal Close { get; set; }
    public double Volume { get; set; }
    public decimal AfterHours { get; set; }
    public decimal PreMarket { get; set; }

    public override string ToString()
    {
        return $"Date: {Date}, Symbol: {Symbol}, Open: {Open}, High: {High}, Low: {Low}, " +
            $"Close: {Close}, Volume: {Volume}, After Hours: {AfterHours}, Pre Market: {PreMarket}";
    }
}