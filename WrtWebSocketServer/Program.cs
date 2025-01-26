using StackExchange.Redis;

using WrtWebSocketServer.Hubs;
using WrtWebSocketServer.Interfaces;
using WrtWebSocketServer.Service;
using WrtWebSocketServer.Workers;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = builder.Configuration.GetConnectionString("Redis");
    return ConnectionMultiplexer.Connect(configuration);
});
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddScoped<ReportService>();
builder.Services.AddScoped<IReport, ReportService>();
builder.Services.AddHostedService<CleaningWorker>();


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

app.Run();
