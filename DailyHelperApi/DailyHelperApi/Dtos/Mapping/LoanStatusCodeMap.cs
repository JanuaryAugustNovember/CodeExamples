
using System;
using Business = DailyHelperApi.Domain.Entities.Enums;
using Dto = DailyHelperApi.Dtos.Enums;

namespace DailyHelperApi.Dtos.Mapping
{
    public static class LoanStatusCodeMap
    {
        public static Business.LoanStatusCode MapToBusinessLoanStatusCode(this Dto.LoanStatusCode statusCode)
        {
            switch (statusCode)
            {
                case Dto.LoanStatusCode.Active:
                    return Business.LoanStatusCode.Active;
                case Dto.LoanStatusCode.PastDue:
                    return Business.LoanStatusCode.PastDue;
                case Dto.LoanStatusCode.Bad:
                    return Business.LoanStatusCode.Bad;
                default:
                    throw new NotImplementedException($"Missing implementation for LoanStatusCode {statusCode}");
            }
        }
    }
}
