using DailyHelperApi.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DailyHelperApi.Domain.Entities.QueryObjects
{
    public class CustomerLoanStatus
    {
        public int LoanId { get; set; }

        public int CustomerId { get; set; }

        public string Email { get; set; }

        public LoanStatusCode Status { get; set; }

        public decimal Balance { get; set; }
    }
}
