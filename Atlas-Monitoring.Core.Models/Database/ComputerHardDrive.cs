namespace Atlas_Monitoring.Core.Models.Database
{
    public class ComputerHardDrive
    {
        public Guid Id { get; set; }
        public Device Device { get; set; }
        public string Letter { get; set; } = string.Empty;
        public double TotalSpace { get; set; } = 0;
        public double SpaceUse { get; set; } = 0;
    }
}