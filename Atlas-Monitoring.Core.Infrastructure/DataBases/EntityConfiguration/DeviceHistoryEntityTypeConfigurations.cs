using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Atlas_Monitoring.Core.Models.Database;

namespace Atlas_Monitoring.Core.Infrastructure.DataBases.EntityConfiguration
{
    internal class DeviceHistoryEntityTypeConfigurations : IEntityTypeConfiguration<DeviceHistory>
    {
        public void Configure(EntityTypeBuilder<DeviceHistory> builder)
        {
            //Define Primary Key
            builder.HasKey(item => item.Id);

            //Define Table name
            builder.ToTable("DeviceHistory");

            //Set property max length
            builder.Property(item => item.Message).HasMaxLength(240);

            //Set default value for properties
            builder.Property(item => item.Message).HasDefaultValue(string.Empty);
        }
    }
}
