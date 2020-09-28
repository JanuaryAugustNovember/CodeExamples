using DailyHelperApi.Domain.Internal;
using DailyHelperApi.Repository;
using DailyHelperApi.Repository.Interfaces;
using DailyHerlperApi.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyHelperApi.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private AppSettings AppSettings;

        private ICustomerRepository _customerRepository;

        private ICustomerApplication _customerApplication;

        public CustomerController(IOptions<AppSettings> appSettings, ICustomerRepository customerRepository, ICustomerApplication customerApplication)
        {
            AppSettings = appSettings.Value ?? throw new ArgumentNullException("AppSettings cannot be null");
            _customerRepository = customerRepository ?? throw new ArgumentNullException("AppSettings cannot be null");
            _customerApplication = customerApplication ?? throw new ArgumentNullException("AppSettings cannot be null");
        }
    }
}
