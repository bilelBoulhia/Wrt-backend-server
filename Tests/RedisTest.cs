using Moq;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using WrtWebSocketServer.Models;
using WrtWebSocketServer.Service;
using Xunit;

public class ReportServiceTests
{
    private readonly Mock<IConnectionMultiplexer> _mockConnectionMultiplexer;
    private readonly Mock<IDatabase> _mockDatabase;
    private readonly ReportService _reportService;

    public ReportServiceTests()
    {
        _mockConnectionMultiplexer = new Mock<IConnectionMultiplexer>();
        _mockDatabase = new Mock<IDatabase>();

        _mockConnectionMultiplexer
            .Setup(c => c.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
            .Returns(_mockDatabase.Object);

        _reportService = new ReportService(_mockConnectionMultiplexer.Object);
    }

    [Fact]
    public async Task InsertReportAsync_ShouldPushReportToRedisList()
    {
  
        var report = new Report
        {
            TrainRoute = "AlgerThenia",
            CurrentGare = "Agha",
            ArrivalHour = DateTime.UtcNow
        };

        var key = $"TrainRoute:{report.TrainRoute}";
        var serializedReport = JsonSerializer.Serialize(report);

        _mockDatabase
            .Setup(db => db.ListRightPushAsync(key, serializedReport, It.IsAny<When>(), It.IsAny<CommandFlags>()))
            .ReturnsAsync(1);

        
        await _reportService.InsertReportAsync(report);

        _mockDatabase.Verify(db => db.ListRightPushAsync(key, serializedReport, It.IsAny<When>(), It.IsAny<CommandFlags>()), Times.Once);
    }

    [Fact]
    public async Task GetReportsByRouteAsync_ShouldReturnListOfReports()
    {
       
        var trainRoute = "AlgerThenia";
        var key = $"TrainRoute:{trainRoute}";

        var reports = new List<Report>
        {
            new Report { TrainRoute = trainRoute, CurrentGare = "Rouiba", ArrivalHour = DateTime.UtcNow },
            new Report { TrainRoute = trainRoute, CurrentGare = "Alger", ArrivalHour = DateTime.UtcNow.AddMinutes(10) }
        };

        var redisValues = reports.Select(r => (RedisValue)JsonSerializer.Serialize(r)).ToArray();

        _mockDatabase
            .Setup(db => db.ListRangeAsync(key, It.IsAny<long>(), It.IsAny<long>(), It.IsAny<CommandFlags>()))
            .ReturnsAsync(redisValues);

    
        var result = await _reportService.GetReportsByRouteAsync(trainRoute);

       
        Assert.Equal(reports.Count, result.Count);
        Assert.Equal(reports[0].TrainRoute, result[0].TrainRoute);
        Assert.Equal(reports[1].TrainRoute, result[1].TrainRoute);

        _mockDatabase.Verify(db => db.ListRangeAsync(key, It.IsAny<long>(), It.IsAny<long>(), It.IsAny<CommandFlags>()), Times.Once);
    }
}
