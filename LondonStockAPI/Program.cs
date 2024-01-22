using System;
using System.IO;
using System.Threading.Tasks;
using RestSharp;
using LondonStockAPI.Controllers;
using LondonStockAPI.Models;
using LondonStockAPI.Services;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

internal class Program
{
    static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

        var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();

        if (appSettings == null || string.IsNullOrEmpty(appSettings.ApiKey))
        {
            throw new InvalidOperationException("AppSettings or ApiKey is missing or empty. Please check your configuration.");
        }

        //Keep in just for debugging
        //Console.WriteLine($"ApiKey from Configuration: {appSettings?.ApiKey}");

        var stockService = new StockService(appSettings?.ApiKey);
        var stockController = new StockController(stockService);

        await stockController.ShowSingularStockInfo();
    }
}