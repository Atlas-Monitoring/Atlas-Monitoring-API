using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Atlas_Monitoring.Core.Models.Database;

namespace Atlas_Monitoring.Core.Infrastructure.DataBases.EntityConfiguration
{
    internal class DeviceEntityTypeConfigurations : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            //Define Primary Key
            builder.HasKey(item => item.Id);

            //Define Table name
            builder.ToTable("Device");

            //Set property max length
            builder.Property(item => item.Name).HasMaxLength(15);
            builder.Property(item => item.Ip).HasMaxLength(15);
            builder.Property(item => item.Domain).HasMaxLength(15);
            builder.Property(item => item.OS).HasMaxLength(35);
            builder.Property(item => item.OSVersion).HasMaxLength(35);
            builder.Property(item => item.UserName).HasMaxLength(48);
            builder.Property(item => item.SerialNumber).HasMaxLength(120);

            //Set default value for properties
            builder.Property(item => item.Name).HasDefaultValue(string.Empty);
            builder.Property(item => item.Ip).HasDefaultValue(string.Empty);
            builder.Property(item => item.Domain).HasDefaultValue(string.Empty);
            builder.Property(item => item.MaxRam).HasDefaultValue(0);
            builder.Property(item => item.NumberOfLogicalProcessors).HasDefaultValue(0);
            builder.Property(item => item.OS).HasDefaultValue(string.Empty);
            builder.Property(item => item.OSVersion).HasDefaultValue(string.Empty);
            builder.Property(item => item.UserName).HasDefaultValue(string.Empty);
            builder.Property(item => item.SerialNumber).HasDefaultValue(string.Empty);
            builder.Property(item => item.DateAdd).HasDefaultValue(DateTime.Now);
            builder.Property(item => item.DateUpdated).HasDefaultValue(DateTime.Now);
        }
    }
}
