using System.Collections.Generic;

namespace BankAccount.Warren.Api.Models.Abstractions
{
    public class PagedResponse<T>
    {
        public List<T> Data { get; set; }

        public int PageSize { get; set; }

        public int TotalRecords { get; set; }

        public int PageNumber { get; set; }
    }
}
