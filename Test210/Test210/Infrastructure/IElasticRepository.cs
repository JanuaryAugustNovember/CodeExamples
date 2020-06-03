
using System.Collections.Generic;

using Test210.Logging;

namespace Test210.Infrastructure
{
    public interface IElasticRepository
    {
        void SaveApiLog(ApiLog log);

        void SaveApplicationLog(ApplicationLog log);

        IEnumerable<ApiLog> SearchApiLogsById(string correlationId);
    }
}
