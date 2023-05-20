using Microsoft.AspNetCore.ResponseCompression;
using rx.core;
using SignalDashBoard;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddHostedService<Worker>();
builder.Services.AddHostedService<TrackWorker>();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});
var app = builder.Build();
//app.MapGet("/", () => "Hello World!");
app.UseResponseCompression();
app.MapHub<SensorHub>("/SensorHub");
app.MapHub<SensorHub>("/TrackHub"); 
app.Run();
