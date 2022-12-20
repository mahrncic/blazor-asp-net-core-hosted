namespace ReportApp.Server.Models.Settings;

public class HostedServiceSettings
{
    public const string SettingName = "HostedService";
    public string BaseAddress { get; set; } = default!;
    public string Uri { get; set; } = default!;
    public string MethodName { get; set; } = default!;
    public int TimeIntervalSeconds { get; set; }
}
