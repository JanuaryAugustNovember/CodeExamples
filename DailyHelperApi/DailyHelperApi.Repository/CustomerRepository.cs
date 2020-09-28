using DailyHelperApi.Domain.Entities;
using DailyHelperApi.Domain.Entities.Enums;
using DailyHelperApi.Domain.Entities.QueryObjects;
using DailyHelperApi.Domain.Internal;
using DailyHelperApi.Repository.Context;
using DailyHelperApi.Repository.Interfaces;
using DailyHelperApi.Repository.Queries;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DailyHelperApi.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private SourceContext _customerContext;

        private AppSettings AppSettings;

        public CustomerRepository(SourceContext customerContext, IOptions<AppSettings> appSettings)
        {
            _customerContext = customerContext;
            AppSettings = appSettings.Value;
        }

        // EF Query
        public Customer GetCustomer(decimal customerId)
        {
            Customer customer;

            try
            {
                // Why is this passing in an empty string instead of null. probably a reason
                using (var conn = new SourceContext(""))
                {
                    customer = _customerContext.Customers
                        .Include(x => x.Loans)
                        .ThenInclude(x => x.LoanStatuses)
                        .FirstOrDefault(x => x.CustomerId == customerId);

                    return customer;
                }
            }
            catch(Exception ex)
            {
                // TODO: log
                throw;
            }
        }

        // EF Query
        public Loan GetLoan(decimal loanId)
        {
            _customerContext.Customers.Include(x => x.Loans);
            _customerContext.Loans.Include(x => x.Customer);

            try
            {
                var loan = _customerContext.Loans
                    .FirstOrDefault(x => x.LoanId == loanId);

                return loan;
            }
            catch (Exception ex)
            {
                // TODO: log
                throw;
            }
        }

        // Dapper with SqlBuilder
        public List<CustomerLoanStatus> GetCustomersByLoanStatus(LoanStatusCode statusCode)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(AppSettings.SourceConnectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("StatusCode", (int)statusCode);

                    var builder = new SqlBuilder().Where(@"statusCode = @StatusCode", new { StatusCode = (int)statusCode });
                    var template = builder.AddTemplate(GetCustomersByStatus.Query);

                    // I don't think the object parameter names matches with the query but it obviously needs to to work.
                    var customers = conn.Query<CustomerLoanStatus>(template.RawSql, parameters).ToList();

                    return customers;
                }
            }
            catch (Exception ex)
            {
                // TODO: log
                throw;
            }

            throw new NotImplementedException();
        }


        // Dapper
        public List<BadCustomer> GetRecentBadCustomers()
        {
            throw new NotImplementedException();
        }
    }
}
