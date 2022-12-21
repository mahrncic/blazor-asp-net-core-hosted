using Microsoft.Extensions.Configuration;
using ReportApp.Server.Hubs;
using ReportApp.Server.Services;
using ReportApp.Server.Workers;
using ReportApp.Shared.Models.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.Configure<HostedServiceSettings>(builder.Configuration.GetSection(HostedServiceSettings.SettingName));
builder.Services.AddScoped<IReportDataService, ReportDataService>();
builder.Services.AddSignalR();
builder.Services.AddHostedService<TimeWorker>();

var allowedOrigins = builder.Configuration.GetValue<string>("AllowedOrigins");
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", opt => opt
        .WithOrigins(allowedOrigins!)
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials());
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseRouting();

app.MapControllers();

var hostedServiceUri
    = builder.Configuration.GetSection(HostedServiceSettings.SettingName).GetValue<string>("Uri");

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ReportHub>(hostedServiceUri!);
});

app.Run();
