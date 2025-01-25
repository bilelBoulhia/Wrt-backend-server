using WrtWebSocketServer.Models;

namespace WrtWebSocketServer.Interfaces
{
    public interface IReport
    {

        public Task InsertReportAsync(Report report);

        Task<List<Report>> GetReportsByRouteAsync(string trainRoute);




    }
}
