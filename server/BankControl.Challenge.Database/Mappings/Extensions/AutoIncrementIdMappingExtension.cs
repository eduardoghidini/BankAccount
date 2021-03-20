using BankAccount.Warren.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BankAccount.Warren.Database.Mappings.Extensions
{
    public static class AutoIncrementIdMappingExtension
    {
        public static void MapAutoIncrementId<T>(this EntityTypeBuilder<T> builder)
            where T : class, IEntity
        {
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id)
                .HasColumnName("id")
                .UseMySqlIdentityColumn();
        }
    }
}
