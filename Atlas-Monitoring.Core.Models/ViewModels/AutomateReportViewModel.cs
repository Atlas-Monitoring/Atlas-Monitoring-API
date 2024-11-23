using Atlas_Monitoring.Core.Models.Internal;

namespace Atlas_Monitoring.Core.Models.ViewModels
{
    public class AutomateReportReadViewModel
    {
        public Guid Id { get; set; }
        public string EntityName { get; set; } = string.Empty;
        public string AppName { get; set; } = string.Empty;
        public AutomateStatus Status { get; set; }
        public string GlobalMessage { get; set; } = string.Empty;
        public double DurationInSeconds { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<AutomateLogReadViewModel> ListOfLogs { get; set; }
    }

    public class AutomateReportWriteViewModel
    {
        public Guid Id { get; set; }
        public string EntityName { get; set; } = string.Empty;
        public string AppName { get; set; } = string.Empty;
        public AutomateStatus Status { get; set; }
        public string GlobalMessage { get; set; } = string.Empty;
        public double DurationInSeconds { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<AutomateLogWriteViewModel> ListOfLogs { get; set; }
    }
}
