namespace Atlas_Monitoring.Core.Models.Database
{
    public class User
    {
        public Guid IdUser { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
