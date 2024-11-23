using Atlas_Monitoring.Core.Models.Internal;

namespace Atlas_Monitoring.Core.Models.ViewModels
{
    public class AutomateLogReadViewModel
    {
        public Guid Id { get; set; }
        public Guid AutomateId { get; set; }
        public string Comment { get; set; } = string.Empty;
        public LogLevel LogLevel { get; set; }
    }

    public class AutomateLogWriteViewModel
    {
        public string Comment { get; set; } = string.Empty;
        public LogLevel LogLevel { get; set; }
    }
}
