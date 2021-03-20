using BankAccount.Warren.Domain.Users;
using BankAccount.Warren.Database.Mappings.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankAccount.Warren.Database.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.MapAutoIncrementId();

            builder
                .Property(_ => _.UserName)
                .HasColumnName("user_name")
                .HasMaxLength(20);

            builder
                 .Property(_ => _.Password)
                 .HasColumnName("password");

            builder
              .HasOne(d => d.Account)
              .WithOne(d => d.User);
        }
    }
}
