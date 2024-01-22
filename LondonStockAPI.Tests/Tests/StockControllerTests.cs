using FluentAssertions;
using LondonStockAPI.Controllers;
using LondonStockAPI.Models;
using LondonStockAPI.Services;
using Moq;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LondonStockAPI.Tests.Tests
{
    [TestFixture]
    public class StockControllerTests
    {
        [Test]
        public async Task ShowStockInfo_WithValidInput_DisplayStockInfo()
        {
            // Arrange
            var stockServiceMock = new Mock<IStockService>();
            stockServiceMock
                .Setup(x => x.GetStockInfoAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new StockServiceResult
                {
                    StockInfo = new StockInfo
                    {
                        Symbol = "AAPL",
                        Open = 200.0m,
                        High = 205.0m,
                        Low = 198.0m,
                        Close = 203.5m,
                        Volume = 1000000.0,
                    },
                    ErrorMessage = null
                });

            var stockController = new StockController(stockServiceMock.Object);

            // Act
            using (var consoleOutput = new ConsoleOutput())
            {
                // Simulate user input
                Console.SetIn(new StringReader("AAPL\n2024-01-19\n"));

                await stockController.ShowSingularStockInfo();

                // Assert
                var output = consoleOutput.GetOutput();
                output.Should().Contain("AAPL");
            }
        }

        [Test]
        public async Task ShowStockInfo_WithInvalidInput_DisplayErrorMessage()
        {
            // Arrange
            var stockServiceMock = new Mock<IStockService>();
            stockServiceMock
                .Setup(x => x.GetStockInfoAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new StockServiceResult
                {
                    StockInfo = null,
                    ErrorMessage = "Could not find stock information for the given inputs."
                });

            var stockController = new StockController(stockServiceMock.Object);

            // Act
            using (var consoleOutput = new ConsoleOutput())
            {
                // Simulate user input
                Console.SetIn(new StringReader("INVALID\ninvalid-date\n"));

                await stockController.ShowSingularStockInfo();

                // Assert
                var output = consoleOutput.GetOutput();
                output.Should().Contain("Failed to retrieve stock information.");
                output.Should().Contain("Could not find stock information for the given inputs.");
            }
        }
    }

    // Setting up the helper class to capture what our console output would be
    public class ConsoleOutput : IDisposable
    {
        private readonly StringWriter _stringWriter;
        private readonly TextWriter _originalOutput;

        public ConsoleOutput()
        {
            _stringWriter = new StringWriter();
            _originalOutput = Console.Out;
            Console.SetOut(_stringWriter);
        }

        public string GetOutput()
        {
            return _stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(_originalOutput);
            _stringWriter.Dispose();
        }
    }
}
