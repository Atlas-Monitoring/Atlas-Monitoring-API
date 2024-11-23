using Atlas_Monitoring.Core.Models.Internal;

namespace Atlas_Monitoring.Core.Models.Database
{
    public class AutomateLog
    {
        public Guid Id { get; set; }
        public AutomateReport AutomateReport { get; set; }
        public string Comment { get; set; } = string.Empty;
        public LogLevel LogLevel { get; set; }
    }
}
