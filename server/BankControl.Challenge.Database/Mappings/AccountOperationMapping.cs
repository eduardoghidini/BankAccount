using BankAccount.Warren.Database.Mappings.Extensions;
using BankAccount.Warren.Domain.AccountOperations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankAccount.Warren.Database.Mappings
{
    public class AccountOperationMapping : IEntityTypeConfiguration<AccountOperation>
    {
        public void Configure(EntityTypeBuilder<AccountOperation> builder)
        {
            builder.ToTable("account_operation");

            builder.MapAutoIncrementId();

            builder
                .Property(_ => _.AccountId)
                .HasColumnName("account_id");

            builder
                .Property(_ => _.Note)
                .HasColumnName("note")
                .HasMaxLength(130)
                .IsRequired(false);

            builder
               .Property(_ => _.Amount)
               .HasColumnName("amount")
               .IsRequired(true);

            builder
               .Property(_ => _.OperationDate)
               .HasColumnName("operation_date")
               .IsRequired(true);

            builder
                .Property(_ => _.OperationType)
                .HasColumnName("operation_type")
                .IsRequired(true);

            builder
              .HasOne(d => d.Account)
              .WithMany(d => d.AccountOperations)
              .HasForeignKey(d => d.AccountId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
