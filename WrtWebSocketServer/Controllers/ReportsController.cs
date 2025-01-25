using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using WrtWebSocketServer.Models;
using WrtWebSocketServer.Service;

namespace WrtWebSocketServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class ReportsController : ControllerBase
    {
        private readonly ReportService _reportService;
        [HttpPost("addReport")]
        public async Task<IActionResult> AddReport(Report report) 
        {
           
            await _reportService.InsertReportAsync(report);
            return Ok();
        }

    }
}
