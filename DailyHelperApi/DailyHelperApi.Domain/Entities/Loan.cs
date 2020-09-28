
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DailyHelperApi.Domain.Entities
{
    public class Loan
    {
        public decimal LoanId { get; set; }

        public decimal CustomerId { get; set; }

        [IgnoreDataMember]
        public Customer Customer { get; set; }

        public ICollection<LoanStatus> LoanStatuses { get; set; }
    }
}
