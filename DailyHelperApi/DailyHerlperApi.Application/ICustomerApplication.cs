using DailyHelperApi.Domain.Entities.Enums;
using DailyHelperApi.Domain.Entities.QueryObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DailyHerlperApi.Application
{
    public interface ICustomerApplication
    {
        List<BadCustomer> GetBadCustomers();

        List<CustomerLoanStatus> GetCustomersByStatus(LoanStatusCode statusCode);
    }
}
