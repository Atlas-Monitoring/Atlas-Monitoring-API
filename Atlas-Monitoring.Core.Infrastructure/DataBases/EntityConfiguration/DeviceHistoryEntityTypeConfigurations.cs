using Atlas_Monitoring.Core.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
