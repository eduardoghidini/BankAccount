using BankAccount.Warren.Domain.AccountOperations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BankAccount.Warren.Database.Mappings.Extensions;

namespace BankAccount.Warren.Database.Mappings
{
    public class AccountOperationRequestMapping : IEntityTypeConfiguration<AccountOperationRequest>
    {
        public void Configure(EntityTypeBuilder<AccountOperationRequest> builder)
        {
            builder.ToTable("account_operation_request");

            builder.MapAutoIncrementId();

            builder
                .Property(_ => _.AccountId)
                .HasColumnName("account_id")
                .IsRequired(true);

            builder
                .Property(_ => _.Note)
                .HasColumnName("note")
                .HasMaxLength(130)
                .IsRequired(false);

            builder
                .Property(_ => _.OperationType)
                .HasColumnName("operation_type")
                .IsRequired(true);

            builder
                .Property(_ => _.Amount)
                .HasColumnName("amount")
                .IsRequired(true);

            builder
                .Property(_ => _.CreationDate)
                .HasColumnName("creation_date")
                .IsRequired(true);

            builder
                .Property(_ => _.OperationDate)
                .HasColumnName("operation_date")
                .IsRequired(true);

            builder
                .Property(_ => _.OperationResponseMessage)
                .HasColumnName("operation_response_message")
                .HasMaxLength(200)
                .IsRequired(false);

            builder
                .Property(_ => _.ProcessedDate)
                .HasColumnName("processed_date")
                .IsRequired(false);


            builder
                .Property(_ => _.JobReferenceId)
                .HasColumnName("job_reference_id")
                .HasMaxLength(15)
                .IsRequired(false);

            builder
                .Property(_ => _.Status)
                .HasColumnName("status")
                .IsRequired(true);

            builder
              .HasOne(d => d.Account)
              .WithMany(d => d.AccountOperationRequest)
              .HasForeignKey(d => d.AccountId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
