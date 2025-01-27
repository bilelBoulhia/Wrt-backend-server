using Moq;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;

using System.Text.Json;
using WrtWebSocketServer.DatabaseContext;
using WrtWebSocketServer.Models;
using WrtWebSocketServer.Service;
using Xunit;

public class ReportServiceTests
{
    private readonly Mock<IConnectionMultiplexer> _mockConnectionMultiplexer;
    private readonly Mock<RedisContext> _mockDatabase;
    private readonly ReportService _reportService;

    //public ReportServiceTests()
    //{
     
    //    _mockDatabase = new Mock<RedisContext>();

    //    _mockConnectionMultiplexer
    //        .Setup(c => c.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
    //        .Returns(_mockDatabase.Object);

    //    _reportService = new ReportService();
    //}

    //[Fact]
    //public async Task InsertReportAsync_ShouldPushReportToRedisList()
    //{
  
    //    var report = new Report
    //    {
    //        TrainRoute = "AlgerThenia",
    //        CurrentGare = "Agha",
       
    //    };

    //    var key = report.TrainRoute;
    //    var serializedReport = JsonSerializer.Serialize(report);

    //    _mockDatabase
    //        .Setup(db => db.ListRightPushAsync(key, serializedReport, It.IsAny<When>(), It.IsAny<CommandFlags>()))
    //        .ReturnsAsync(1);

        
    //    await _reportService.InsertReportAsync(report);

    //    _mockDatabase.Verify(db => db.ListRightPushAsync(key, serializedReport, It.IsAny<When>(), It.IsAny<CommandFlags>()), Times.Once);
    //}

    //[Fact]
    //public async Task GetReportsByRouteAsync_ShouldReturnListOfReports()
    //{
       
        
    //    var key = "AlgerThenia";

    //    var reports = new List<Report>
    //    {
    //        new Report { TrainRoute = key, CurrentGare = "Rouiba"  },
    //        new Report { TrainRoute = key, CurrentGare = "Alger" }
    //    };

    //    var redisValues = reports.Select(r => (RedisValue)JsonSerializer.Serialize(r)).ToArray();

    //    _mockDatabase
    //        .Setup(db => db.ListRangeAsync(key, It.IsAny<long>(), It.IsAny<long>(), It.IsAny<CommandFlags>()))
    //        .ReturnsAsync(redisValues);

    
    //    var result = await _reportService.GetReportsByRouteAsync(key);

       
    //    Assert.Equal(reports.Count, result.Count);
    //    Assert.Equal(reports[0].TrainRoute, result[0].TrainRoute);
    //    Assert.Equal(reports[1].TrainRoute, result[1].TrainRoute);

    //    _mockDatabase.Verify(db => db.ListRangeAsync(key, It.IsAny<long>(), It.IsAny<long>(), It.IsAny<CommandFlags>()), Times.Once);
    //}
}
