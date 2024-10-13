using Atlas_Monitoring.Core.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas_Monitoring.Core.Infrastructure.DataBases.EntityConfiguration
{
    internal class EntityEntityTypeConfigurations : IEntityTypeConfiguration<Entity>
    {
        public void Configure(EntityTypeBuilder<Entity> builder)
        {
            //Define Primary Key
            builder.HasKey(item => item.EntityId);

            //Define Table name
            builder.ToTable("Entity");

            //Set property max length
            builder.Property(item => item.Name).HasMaxLength(70);

            //Set default value for properties
            builder.Property(item => item.Name).HasDefaultValue(string.Empty);
        }
    }
}
