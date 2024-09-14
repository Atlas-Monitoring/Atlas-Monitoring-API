using Atlas_Monitoring.Core.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas_Monitoring.Core.Infrastructure.DataBases.EntityConfiguration
{
    internal class DevicePartsEntityTypeConfigurations : IEntityTypeConfiguration<DeviceParts>
    {
        public void Configure(EntityTypeBuilder<DeviceParts> builder)
        {
            //Define Primary Key
            builder.HasKey(item => item.Id);

            //Define Table name
            builder.ToTable("DeviceParts");

            //Set property max length
            builder.Property(item => item.Name).HasMaxLength(40);
            builder.Property(item => item.Labels).HasMaxLength(120);

            //Set default value for properties
            builder.Property(item => item.Name).HasDefaultValue(string.Empty);
            builder.Property(item => item.Labels).HasDefaultValue(string.Empty);
        }
    }
}
