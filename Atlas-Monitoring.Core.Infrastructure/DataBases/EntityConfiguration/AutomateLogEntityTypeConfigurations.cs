using Atlas_Monitoring.Core.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas_Monitoring.Core.Infrastructure.DataBases.EntityConfiguration
{
    internal class AutomateLogEntityTypeConfigurations : IEntityTypeConfiguration<AutomateLog>
    {
        public void Configure(EntityTypeBuilder<AutomateLog> builder)
        {
            //Define Primary Key
            builder.HasKey(item => item.Id);

            //Define Table name
            builder.ToTable("AutomateLog");

            builder.Property(item => item.Comment).HasMaxLength(240);

            //Set default value for properties
            builder.Property(item => item.Comment).HasDefaultValue(string.Empty);
        }
    }
}
