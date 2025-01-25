using StackExchange.Redis;
using System.Configuration;
using WrtWebSocketServer.Hubs;
using WrtWebSocketServer.Service;
using WrtWebSocketServer.Workers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<ReportWorker>();
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
builder.Services.AddSignalR();

app.MapHub<WrtHub>("/hubs/WrtHub");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
