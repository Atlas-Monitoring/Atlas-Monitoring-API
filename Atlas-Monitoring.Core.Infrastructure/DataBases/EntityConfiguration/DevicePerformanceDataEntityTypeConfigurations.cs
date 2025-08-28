using Atlas_Monitoring.Core.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas_Monitoring.Core.Infrastructure.DataBases.EntityConfiguration
{
    internal class DevicePerformanceDataEntityTypeConfigurations : IEntityTypeConfiguration<DevicePerformanceData>
    {
        public void Configure(EntityTypeBuilder<DevicePerformanceData> builder)
        {
            //Define Primary Key
            builder.HasKey(item => item.Id);

            //Define Table name
            builder.ToTable("ComputerData");

            //Set default value for properties
            builder.Property(item => item.ProcessorUtilityPourcent).HasDefaultValue(0);
            builder.Property(item => item.MemoryUsed).HasDefaultValue(0);
            builder.Property(item => item.UptimeSinceInSecond).HasDefaultValue(0);
        }
    }
}
