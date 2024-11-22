using Atlas_Monitoring.Core.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas_Monitoring.Core.Infrastructure.DataBases.EntityConfiguration
{
    internal class AutomateReportEntityTypeConfigurations : IEntityTypeConfiguration<AutomateReport>
    {
        public void Configure(EntityTypeBuilder<AutomateReport> builder)
        {
            //Define Primary Key
            builder.HasKey(item => item.Id);

            //Define Table name
            builder.ToTable("AutomateReport");

            builder.Property(item => item.AppName).HasMaxLength(60);
            builder.Property(item => item.GlobalMessage).HasMaxLength(240);

            //Set default value for properties
            builder.Property(item => item.AppName).HasDefaultValue(string.Empty);
            builder.Property(item => item.GlobalMessage).HasDefaultValue(string.Empty);
        }
    }
}
