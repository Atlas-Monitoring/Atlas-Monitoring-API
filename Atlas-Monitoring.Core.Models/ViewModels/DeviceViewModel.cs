using Atlas_Monitoring.Core.Models.Database;
using Atlas_Monitoring.Core.Models.Internal;

namespace Atlas_Monitoring.Core.Models.ViewModels
{
    public class DeviceViewModel
    {
        public Guid Id { get; set; }
        public DeviceStatus DeviceStatus { get; set; } = DeviceStatus.New;
        public DeviceType DeviceType { get; set; } = DeviceType.Computer;
        public string Name { get; set; } = string.Empty;
        public string Ip { get; set; } = string.Empty;
        public string Domain { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public DateTime DateAdd { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
