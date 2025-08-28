using Atlas_Monitoring.Core.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas_Monitoring.Core.Infrastructure.DataBases.EntityConfiguration
{
    internal class DeviceHardDriveEntityTypeConfigurations : IEntityTypeConfiguration<DeviceHardDrive>
    {
        public void Configure(EntityTypeBuilder<DeviceHardDrive> builder)
        {
            //Define Primary Key
            builder.HasKey(item => item.Id);

            //Define Table name
            builder.ToTable("ComputerHardDrive");

            //Set property max length
            builder.Property(item => item.Letter).HasMaxLength(2);

            //Set default value for properties
            builder.Property(item => item.Letter).HasDefaultValue(string.Empty);
            builder.Property(item => item.TotalSpace).HasDefaultValue(0);
            builder.Property(item => item.SpaceUse).HasDefaultValue(0);
        }
    }
}
