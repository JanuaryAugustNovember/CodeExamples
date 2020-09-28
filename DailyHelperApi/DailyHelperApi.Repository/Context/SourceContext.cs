using DailyHelperApi.Domain.Entities;
using DailyHelperApi.Domain.Internal;
using DailyHelperApi.Repository.Context.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace DailyHelperApi.Repository.Context
{
    public class SourceContext : DbContext
    {
        private AppSettings AppSettings;

        private string ConnectionString;

        public SourceContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public SourceContext(IOptions<AppSettings> appSettings)
        {
            AppSettings = appSettings.Value;
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.MapCustomer();
            modelBuilder.MapLoan();
            modelBuilder.MapLoanStatus();
        }

        // used to log actual sql queries to the output window while debugging.
        public static readonly Microsoft.Extensions.Logging.LoggerFactory _loggerFactory = new Microsoft.Extensions.Logging.LoggerFactory(
            new[]
            {
                new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
            });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString ?? AppSettings.SourceConnectionString);
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }
    }
}
