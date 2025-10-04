namespace Atlas_Monitoring.Core.Models.ViewModels
{
    public class DeviceHardDriveViewModel
    {
        public Guid Id { get; set; }
        public Guid DeviceId { get; set; }
        public string Letter { get; set; } = string.Empty;
        public double TotalSpace { get; set; } = 0;
        public double SpaceUse { get; set; } = 0;
    }
}
