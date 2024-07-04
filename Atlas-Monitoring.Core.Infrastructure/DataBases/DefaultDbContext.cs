﻿using Atlas_Monitoring.Core.Models.Database;
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
            modelBuilder.ApplyConfiguration(new EntityConfiguration.ComputerDataEntityTypeConfigurations());
            modelBuilder.ApplyConfiguration(new EntityConfiguration.ComputerHardDriveEntityTypeConfigurations());
            modelBuilder.ApplyConfiguration(new EntityConfiguration.DeviceEntityTypeConfigurations());
            modelBuilder.ApplyConfiguration(new EntityConfiguration.DeviceHistoryEntityTypeConfigurations());
            modelBuilder.ApplyConfiguration(new EntityConfiguration.DeviceTypeEntityTypeConfigurations());
        }

        #endregion

        #region Propriétés
        public DbSet<Device> Device { get; set; }
        public DbSet<ComputerData> ComputerData { get; set; }
        public DbSet<ComputerHardDrive> ComputerHardDrive { get; set; }
        public DbSet<DeviceHistory> DeviceHistory { get; set; }
        public DbSet<DeviceType> DeviceType { get; set; }
        #endregion
    }
}
