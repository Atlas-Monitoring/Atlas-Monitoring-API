namespace Atlas_Monitoring.Core.Models.ViewModels
{
    public class DeviceHistoryViewModel
    {
        public Guid Id { get; set; }
        public DateTime DateAdd { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
