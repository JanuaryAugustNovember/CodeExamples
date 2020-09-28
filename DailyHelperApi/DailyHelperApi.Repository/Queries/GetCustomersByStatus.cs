
namespace DailyHelperApi.Repository.Queries
{
    public class GetCustomersByStatus
    {
        public static string Query =
            @"select
                LoanId
                ,CustomerId
                ,Email
                ,Status
                ,EffectiveDate
            from loans
                --joins here
            /**where**/"
    }
}
