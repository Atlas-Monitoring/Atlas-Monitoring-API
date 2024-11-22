using Atlas_Monitoring.Core.Models.Internal;

namespace Atlas_Monitoring.Core.Models.Database
{
    public class AutomateReport
    {
        public Guid Id { get; set; }
        public Entity? Entity { get; set; }
        public string AppName { get; set; } = string.Empty;
        public AutomateStatus Status { get; set; }
        public string GlobalMessage { get; set; } = string.Empty;
        public double DurationInSeconds { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
