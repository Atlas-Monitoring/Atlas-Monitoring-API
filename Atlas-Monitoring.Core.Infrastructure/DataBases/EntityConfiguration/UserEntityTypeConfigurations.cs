using Atlas_Monitoring.Core.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas_Monitoring.Core.Infrastructure.DataBases.EntityConfiguration
{
    internal class UserEntityTypeConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Define Primary Key
            builder.HasKey(item => item.IdUser);

            //Define Table name
            builder.ToTable("User");

            //Set max length for properties
            builder.Property(item => item.UserName).HasMaxLength(35);
            builder.Property(item => item.Password).HasMaxLength(60);

            //Add 
            User user = new User()
            {
                IdUser = new Guid("2f1d2516-7085-40a9-bca0-3fb5ef613d2a"),
                UserName = "admin",
                Password = "$2a$11$gD72tcJt3RGEgPt0v9gR4O1PPwR8Koc25ssq5g1Bg4mq8ycUwF7Sm"
            };

            builder.HasData(user);
        }
    }
}
