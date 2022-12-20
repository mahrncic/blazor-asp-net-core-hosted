using ReportApp.Server.Hubs;
using ReportApp.Server.Models.Settings;
using ReportApp.Server.Services;
using ReportApp.Server.Workers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped<IReportDataService, ReportDataService>();
builder.Services.AddSignalR();
builder.Services.AddHostedService<TimeWorker>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();

var hostedServiceUri
    = builder.Configuration.GetSection(HostedServiceSettings.SettingName).GetValue<string>("Uri");

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ReportHub>(hostedServiceUri!);
});

app.Run();
