using System.Net;
using FluentAssertions;
using LondonStockAPI.Models;
using LondonStockAPI.Services;
using Moq;
using NUnit.Framework;
using RestSharp;

namespace LondonStockAPI.Tests
{
    [TestFixture]
    public class StockServiceTests
    {
        private const string ValidApiKey = "QuzUdor3kBYzVDRvVbqNsS3LJ4T_nmwR";

        private IStockService CreateStockService(string apiKey = ValidApiKey)
        {
            return new StockService(apiKey);
        }

        [Test]
        public async Task GetStockInfoAsync_WithValidSymbolAndDate_ReturnsStockInfo()
        {
            // Arrange
            var _IStockService = CreateStockService();

            // Act
            var result = await _IStockService.GetStockInfoAsync("AAPL", "2024-01-19");

            // Assert
            result.StockInfo.Should().NotBeNull();
            result.StockInfo.Symbol.Should().Be("AAPL");
            result.StockInfo.Open.Should().Be(189.33m);
            result.StockInfo.High.Should().Be(191.95m);
            result.StockInfo.Low.Should().Be(188.82m);
            result.StockInfo.Close.Should().Be(191.56m);
            result.StockInfo.Volume.Should().Be(68887985.0);
            result.StockInfo.AfterHours.Should().Be(191.23m);
            result.StockInfo.PreMarket.Should().Be(188.89m);
        }

        [Test]
        public async Task GetStockInfoAsync_WithInvalidSymbol_ThrowsInvalidOperationException()
        {
            // Arrange
            var _IStockService = CreateStockService();

            // Act
            var result = await _IStockService.GetStockInfoAsync("INVALID", "2024-01-19");

            // Assert
            result.StockInfo.Should().BeNull();
            result.ErrorMessage.Should().Be("Could not find stock that matches ticker INVALID. Please try again");
        }

        [Test]
        public async Task GetStockInfoAsync_WithUnauthorizedResponse_ReturnsUnauthorizedError()
        {
            // Arrange
            var _IStockService = CreateStockService(apiKey: "INCORRECT");

            // Act
            var result = await _IStockService.GetStockInfoAsync("AAPL", "2024-01-19");

            // Assert
            result.StockInfo.Should().BeNull();
            result.ErrorMessage.Should().Be("Invalid API key. Please check your API key and try again");
        }

        [Test]
        public async Task GetStockInfoAsync_WithInvalidDate_Throws_BadRequest_Exception()
        {
            // Arrange
            var _IStockService = CreateStockService();

            // Act
            var result = await _IStockService.GetStockInfoAsync("AAPL", "invalid-date");

            // Assert
            result.StockInfo.Should().BeNull();
            result.ErrorMessage.Should().StartWith("Failed to retrieve stock information. Status code: BadRequest");
        }
    }
}