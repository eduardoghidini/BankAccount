using System.Collections.Generic;

namespace BankAccount.Warren.Application.Abstractions
{
    public class PagedResult<T>
    {
        public List<T> Data { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public int PageNumber { get; set; }
    }
}
