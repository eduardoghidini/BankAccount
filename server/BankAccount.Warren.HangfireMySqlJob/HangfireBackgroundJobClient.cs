using BankAccount.Warren.Domain.Abstractions;
using Hangfire.States;
using System;
using System.Linq.Expressions;
using Hangfire;

namespace BankAccount.Warren.HangfireMySqlJob
{
    public class HangfireBackgroundJobClient : IJobClient
    {
        private readonly IBackgroundJobClient _client;

        public HangfireBackgroundJobClient(IBackgroundJobClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public string Enqueue<T>(Expression<Action<T>> methodCall, DateTime? executionDate = null)
        {
            if (methodCall == null)
            {
                throw new ArgumentNullException(nameof(methodCall));
            }

            IState state = executionDate.HasValue ? (IState)new ScheduledState(executionDate.Value) : new EnqueuedState();

            return _client.Create(methodCall, state);
        }

        public bool Delete(string jobId)
        {
            return _client.Delete(jobId);
        }
    }
}
