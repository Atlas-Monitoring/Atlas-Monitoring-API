using Atlas_Monitoring.Core.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Atlas_Monitoring.Core.Infrastructure.DataBases
{
    public class DefaultDbContext : DbContext
    {

        #region Contructeurs
        public DefaultDbContext(DbContextOptions options) : base(options)
        {

        }

        public DefaultDbContext()
        {

        }
        #endregion

        #region Public Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EntityConfiguration.AutomateLogEntityTypeConfigurations());
            modelBuilder.ApplyConfiguration(new EntityConfiguration.AutomateReportEntityTypeConfigurations());
            modelBuilder.ApplyConfiguration(new EntityConfiguration.DevicePerformanceDataEntityTypeConfigurations());
            modelBuilder.ApplyConfiguration(new EntityConfiguration.DeviceHardDriveEntityTypeConfigurations());
            modelBuilder.ApplyConfiguration(new EntityConfiguration.DeviceEntityTypeConfigurations());
            modelBuilder.ApplyConfiguration(new EntityConfiguration.DeviceHistoryEntityTypeConfigurations());
            modelBuilder.ApplyConfiguration(new EntityConfiguration.DevicePartsEntityTypeConfigurations());
            modelBuilder.ApplyConfiguration(new EntityConfiguration.DeviceSoftwareInstalledEntityTypeConfigurations());
            modelBuilder.ApplyConfiguration(new EntityConfiguration.DeviceTypeEntityTypeConfigurations());
            modelBuilder.ApplyConfiguration(new EntityConfiguration.EntityEntityTypeConfigurations());
            modelBuilder.ApplyConfiguration(new EntityConfiguration.UserEntityTypeConfigurations());

            //Disable Delete cascade
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        #endregion

        #region Propriétés
        public DbSet<AutomateLog> AutomateLog { get; set; }
        public DbSet<AutomateReport> AutomateReport { get; set; }
        public DbSet<Device> Device { get; set; }
        public DbSet<DevicePerformanceData> DevicePerformanceData { get; set; }
        public DbSet<DeviceHardDrive> DeviceHardDrive { get; set; }
        public DbSet<DeviceHistory> DeviceHistory { get; set; }
        public DbSet<DeviceParts> DeviceParts { get; set; }
        public DbSet<DeviceSoftwareInstalled> DeviceSoftwareInstalled { get; set; }
        public DbSet<DeviceType> DeviceType { get; set; }
        public DbSet<Entity> Entity { get; set; }
        public DbSet<User> User { get; set; }
        #endregion
    }
}
