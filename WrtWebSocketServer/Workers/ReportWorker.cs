﻿
namespace WrtWebSocketServer.Workers
{
    public class ReportWorker : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
