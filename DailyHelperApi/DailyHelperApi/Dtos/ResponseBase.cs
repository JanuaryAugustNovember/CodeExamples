using System;
using System.Net;

namespace DailyHelperApi.Dtos
{
    public class ResponseBase
    {
        public Guid CorrelationId { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }

        public string ErrorMessage { get; set; }
    }
}
