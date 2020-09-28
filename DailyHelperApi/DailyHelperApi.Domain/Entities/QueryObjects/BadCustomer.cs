using System;
using System.Collections.Generic;
using System.Text;

namespace DailyHelperApi.Domain.Entities.QueryObjects
{
    public class BadCustomer
    {
        public int LoanId { get; set; }
        
        public int CustomerId { get; set; }

        public string Email { get; set; }

        public string Status { get; set; }

        public decimal Balance { get; set; }
    }
}
