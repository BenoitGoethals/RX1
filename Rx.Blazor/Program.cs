using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Rx.Blazor.Data;
using rx.core;
using rx.core.chat;
using Syncfusion.Blazor;
using Syncfusion.Licensing;
using Microsoft.AspNetCore.ResponseCompression;
using Rx.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSyncfusionBlazor();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSignalR(configure: options => { options.EnableDetailedErrors = true;});
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<SensorObs>();
builder.Services.AddSingleton<ChatServer>();
builder.Services.AddScoped<ISensorSignalRClient, SensorSignalRClient>();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

var app = builder.Build();
app.UseResponseCompression();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt+QHFqUUdrXVNbdV5dVGpAd0N3RGlcdlR1fUUmHVdTRHRcQllhQH5ack1gUXlZd3E=;Mgo+DSMBPh8sVXJ1S0d+WFBPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSXpRf0VqXXddeHNWQ2U=;ORg4AjUWIQA/Gnt2VFhhQlVFfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5XdExjUHtWc3xSQ2BZ;MTg3NTIxOUAzMjMxMmUzMTJlMzQzMU9HdGhocHRpSzl2WFlZNkFJY3ZTZGF1bXRIQ0hYTFdXYVhrSmg0QVlLUkU9;MTg3NTIyMEAzMjMxMmUzMTJlMzQzMUtRNTFVSEVvbVFTWDBMQjJqcHhGUE02R2ZaaUpydlVod3FRbU44ZmpSYm89;NRAiBiAaIQQuGjN/V0d+XU9Ad1RDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS31TckdqWXZaeHddTmFZVA==;MTg3NTIyMkAzMjMxMmUzMTJlMzQzMVQyMk9YQ2tvanFlUUQ0Mk1iZHRJMmZsUFFtakloZGFxRkJFS2RBSWdFR3M9;MTg3NTIyM0AzMjMxMmUzMTJlMzQzMWRyR2lNeUVaczFXTEVUOVBNUGQwcTRoS1VzTStYcEZDMThMWkRQN3EyT2M9;Mgo+DSMBMAY9C3t2VFhhQlVFfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5XdExjUHtWc3xdTmdZ;MTg3NTIyNUAzMjMxMmUzMTJlMzQzMUxBcktOSytYYTZ1T3FYc2FUTmNiVEhmR1BKMC9RTU9vSEplUTcrUnRWTWs9;MTg3NTIyNkAzMjMxMmUzMTJlMzQzMWI5YVhxQnhETnoxdjhmV0FLSmxjdWxVNmd2Qnh4K3NOSWxKcWRXQk94Rlk9;MTg3NTIyN0AzMjMxMmUzMTJlMzQzMVQyMk9YQ2tvanFlUUQ0Mk1iZHRJMmZsUFFtakloZGFxRkJFS2RBSWdFR3M9");


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
