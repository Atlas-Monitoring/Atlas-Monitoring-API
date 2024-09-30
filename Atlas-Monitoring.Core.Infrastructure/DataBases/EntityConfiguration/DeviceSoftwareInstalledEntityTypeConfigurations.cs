using Atlas_Monitoring.Core.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas_Monitoring.Core.Infrastructure.DataBases.EntityConfiguration
{
    internal class DeviceSoftwareInstalledEntityTypeConfigurations : IEntityTypeConfiguration<DeviceSoftwareInstalled>
    {
        public void Configure(EntityTypeBuilder<DeviceSoftwareInstalled> builder)
        {
            //Define Primary Key
            builder.HasNoKey();

            //Define Table name
            builder.ToTable("DeviceSoftwareInstalled");

            //Set property max length
            builder.Property(item => item.AppName).HasMaxLength(240);
            builder.Property(item => item.Version).HasMaxLength(240);
            builder.Property(item => item.Publisher).HasMaxLength(240);

            //Set default value for properties
            builder.Property(item => item.AppName).HasDefaultValue(string.Empty);
            builder.Property(item => item.Version).HasDefaultValue(string.Empty);
            builder.Property(item => item.Publisher).HasDefaultValue(string.Empty);
        }
    }
}
