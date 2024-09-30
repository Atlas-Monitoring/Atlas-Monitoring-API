namespace Atlas_Monitoring.Core.Models.ViewModels
{
    public class DeviceSoftwareInstalledReadViewModel
    {
        public string AppName { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
    }

    public class DeviceSoftwareInstalledWriteViewModel
    {
        public Guid DeviceId { get; set; }
        public string AppName { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
    }
}
