namespace Atlas_Monitoring.Core.Models.Database
{
    public class DevicePerformanceData
    {
        public Guid Id { get; set; }
        public Device Device { get; set; }
        public DateTime DateAdd { get; set; } = DateTime.Now;
        public double ProcessorUtilityPourcent { get; set; } = 0;
        public double MemoryUsed { get; set; } = 0;
        public double UptimeSinceInSecond { get; set; } = 0;
    }
}