using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Atlas_Monitoring.Core.Models.Database;

namespace Atlas_Monitoring.Core.Infrastructure.DataBases.EntityConfiguration
{
    internal class DeviceTypeEntityTypeConfigurations : IEntityTypeConfiguration<DeviceType>
    {
        public void Configure(EntityTypeBuilder<DeviceType> builder)
        {
            //Define Primary Key
            builder.HasKey(item => item.Id);

            //Define Table name
            builder.ToTable("DeviceType");

            //Set property max length
            builder.Property(item => item.Name).HasMaxLength(35);

            //Set default value for properties
            builder.Property(item => item.Name).HasDefaultValue(string.Empty);

            //Add data
            builder.HasData(
                DeviceType.Undefined,
                DeviceType.Computer,
                DeviceType.Server,
                DeviceType.Printer,
                DeviceType.Router,
                DeviceType.Switch,
                DeviceType.Phone,
                DeviceType.MobilePhone,
                DeviceType.Other
            );
        }
    }
}
