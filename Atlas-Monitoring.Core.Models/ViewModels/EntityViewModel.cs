namespace Atlas_Monitoring.Core.Models.ViewModels
{
    public class EntityReadViewModel
    {
        public Guid EntityId { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class EntityWriteViewModel
    {
        public Guid EntityId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
