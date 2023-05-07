using Microsoft.AspNetCore.ResponseCompression;
using SignalDashBoard;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddHostedService<Worker>();
//builder.Services.AddResponseCompression(opts =>
//{
//    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
//        new[] { "application/octet-stream" });
//});
var app = builder.Build();
app.MapGet("/", () => "Hello World!");
//app.UseResponseCompression();
app.MapHub<SensorHub>("/SensorHub");
app.Run();
