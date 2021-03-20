using System;
using System.Linq.Expressions;

namespace BankAccount.Warren.Domain.Abstractions
{
    public interface IJobClient
    {
        string Enqueue<T>(Expression<Action<T>> methodCall, DateTime? executionDate = null);

        bool Delete(string jobId);
    }
}
