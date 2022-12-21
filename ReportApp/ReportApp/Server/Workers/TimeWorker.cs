using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using ReportApp.Server.Hubs;
using ReportApp.Server.Services;
using ReportApp.Shared.Models.Settings;

namespace ReportApp.Server.Workers;

public class TimeWorker : BackgroundService
{
    private readonly ILogger<TimeWorker> _logger;
    private readonly IHubContext<ReportHub> _reportHub;
    private readonly HostedServiceSettings _settings;
    private readonly IServiceProvider _serviceProvider;

    public TimeWorker(
        ILogger<TimeWorker> logger,
        IHubContext<ReportHub> reportHub,
        IOptions<HostedServiceSettings> settings,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _reportHub = reportHub;
        _settings = settings.Value;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var reportDataService = scope.ServiceProvider.GetRequiredService<IReportDataService>();

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Time Worker running at: {Time}", DateTime.Now);
            var reportData = await reportDataService.GetReports();

            await _reportHub.Clients.All.SendAsync(
                _settings.MethodName,
                reportData,
                stoppingToken);

            await Task.Delay(
                TimeSpan.FromSeconds(_settings.TimeIntervalSeconds),
                stoppingToken);
        }
    }
}
