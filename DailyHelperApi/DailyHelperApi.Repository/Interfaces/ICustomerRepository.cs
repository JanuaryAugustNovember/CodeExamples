using DailyHelperApi.Domain.Entities;
using DailyHelperApi.Domain.Entities.Enums;
using DailyHelperApi.Domain.Entities.QueryObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DailyHelperApi.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        Customer GetCustomer(decimal customerId);

        Loan GetLoan(decimal loanId);

        List<BadCustomer> GetRecentBadCustomers();

        List<CustomerLoanStatus> GetCustomersByLoanStatus(LoanStatusCode statusCode);
    }
}
