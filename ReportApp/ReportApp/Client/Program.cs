using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ReportApp.Client;
using ReportApp.Shared.Models.Settings;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Configuration.AddJsonFile("wwwroot/appsettings.json", optional: true, reloadOnChange: true);

builder.Services.Configure<HostedServiceSettings>(builder.Configuration.GetSection("HostedService"));

await builder.Build().RunAsync();
