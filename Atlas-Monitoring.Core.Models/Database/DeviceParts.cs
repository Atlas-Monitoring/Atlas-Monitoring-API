namespace Atlas_Monitoring.Core.Models.Database
{
    public class DeviceParts
    {
        public Guid Id { get; set; }
        public Device Device { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Labels { get; set; } = string.Empty;
    }
}
