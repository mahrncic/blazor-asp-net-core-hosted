using Microsoft.AspNetCore.SignalR;
using ReportApp.Shared;

namespace ReportApp.Server.Hubs;

public class ReportHub : Hub<List<Report>>
{
}
