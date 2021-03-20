using BankAccount.Warren.Database.Mappings.Extensions;
using BankAccount.Warren.Domain.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankAccount.Warren.Database.Mappings
{
    public class AccountMapping : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("account");

            builder.MapAutoIncrementId();

            builder.Property(_ => _.OwnerName)
                .HasColumnName("owner_name")
                .HasMaxLength(500)
                .IsRequired(true);

            builder.Property(_ => _.CurrentBalance)
                .HasColumnName("current_balance")
                .IsRequired(true);

            builder.Property(_ => _.ApplyiedBalance)
                .HasColumnName("applied_balance")
                .IsRequired(true);

            builder.Property(_ => _.AccountNumber)
                .HasColumnName("account_number")
                .HasMaxLength(20)
                .IsRequired(true);

            builder.Property(_ => _.UserId)
                .HasColumnName("user_id")
                .IsRequired(true);

            builder
               .HasMany(d => d.AccountOperationRequest)
               .WithOne(d => d.Account);

            builder
               .HasMany(d => d.AccountOperations)
               .WithOne(d => d.Account);

            builder
                .HasOne(d => d.User)
                .WithOne(d => d.Account)
                .HasForeignKey<Account>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
