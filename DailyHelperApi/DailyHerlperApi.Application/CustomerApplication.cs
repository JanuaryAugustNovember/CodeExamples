using DailyHelperApi.Domain.Entities.Enums;
using DailyHelperApi.Domain.Entities.QueryObjects;
using DailyHelperApi.Domain.Internal;
using DailyHelperApi.Repository.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace DailyHerlperApi.Application
{
    public class CustomerApplication : ICustomerApplication
    {
        private AppSettings AppSettings { get; set; }

        private ICustomerRepository _customerRepository { get; }

        public CustomerApplication(IOptions<AppSettings> options, ICustomerRepository customerRepository)
        {
            AppSettings = options.Value ?? throw new ArgumentNullException("AppSettings cannot be null");
            _customerRepository = customerRepository ?? throw new ArgumentNullException("Customer Repository cannot be null");
        }

        public List<BadCustomer> GetBadCustomers()
        {
            return _customerRepository.GetRecentBadCustomers();
        }

        public List<CustomerLoanStatus> GetCustomersByStatus(LoanStatusCode statusCode)
        {
            return _customerRepository.GetCustomersByLoanStatus(statusCode);
        }
    }
}
