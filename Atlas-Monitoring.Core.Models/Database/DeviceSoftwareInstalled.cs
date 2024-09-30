namespace Atlas_Monitoring.Core.Models.Database
{
    public class DeviceSoftwareInstalled
    {
        public Device Device { get; set; }
        public string AppName { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
    }
}
