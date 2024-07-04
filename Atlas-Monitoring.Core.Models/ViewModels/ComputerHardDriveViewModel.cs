namespace Atlas_Monitoring.Core.Models.ViewModels
{
    public class ComputerHardDriveViewModel
    {
        public Guid Id { get; set; }
        public DateTime DateAdd { get; set; } = DateTime.Now;
        public string Letter { get; set; } = string.Empty;
        public double TotalSpace { get; set; } = 0;
        public double SpaceUse { get; set; } = 0;
    }
}
