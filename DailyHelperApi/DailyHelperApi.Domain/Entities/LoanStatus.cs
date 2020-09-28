using System;
using System.Runtime.Serialization;

namespace DailyHelperApi.Domain.Entities
{
    public class LoanStatus
    {
        public decimal Id { get; set; }

        public decimal LoanId { get; set; }

        public decimal StatusCode { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime EntryDate { get; set; }

        [IgnoreDataMember]
        public Loan Loan { get; set; }
    }
}
