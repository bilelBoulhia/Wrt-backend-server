using StackExchange.Redis;
using WrtWebSocketServer.DatabaseContext;
using WrtWebSocketServer.Handlers;
using WrtWebSocketServer.Hubs;
using WrtWebSocketServer.Interfaces;
using WrtWebSocketServer.Service;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<RedisContext>();
builder.Services.AddSingleton<SpamHandler>();
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = builder.Configuration.GetConnectionString("Redis");
    return ConnectionMultiplexer.Connect(configuration);
});

builder.Services.AddSignalR();
builder.Services.AddScoped<ReportService>();
builder.Services.AddScoped<IReport, ReportService>();



var app = builder.Build();


if (app.Environment.IsDevelopment())
{

    app.UseDeveloperExceptionPage();
}
else
{
  
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseCors(builder =>
    builder.AllowAnyOrigin()  
           .AllowAnyMethod()
           .AllowAnyHeader()
           );


app.MapHub<WrtHub>("/wrtHub");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapControllers();

app.Run();
