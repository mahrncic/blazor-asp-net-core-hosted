using ReportApp.Shared;

namespace ReportApp.Server.Services;

public interface IReportDataService
{
    Task<List<Report>> GetReports();
}
