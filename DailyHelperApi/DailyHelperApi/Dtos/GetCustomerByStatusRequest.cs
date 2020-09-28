using DailyHelperApi.Dtos.Enums;

namespace DailyHelperApi.Dtos
{
    public class GetCustomerByStatusRequest
    {
        public LoanStatusCode StatusCode { get; set; }
    }
}
