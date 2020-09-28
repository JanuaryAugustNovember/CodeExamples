
using DailyHelperApi.Domain.Entities;

namespace DailyHelperApi.Dtos
{
    public class GetCustomerResponse : ResponseBase
    {
        public Customer Customer { get; set; }
    }
}
