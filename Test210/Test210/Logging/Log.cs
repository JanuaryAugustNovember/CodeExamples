
using System;

namespace Test210.Logging
{
    public class ApiLog
    {
        public Guid CorrelationId { get; set; }

        public LogType LogType { get; set; }

        public DateTime Timestamp { get; set; }

        public string Version { get; set; }

        public string Path { get; set; }

        public string Method { get; set; }

        public string RequestBody { get; set; }

        public string ResponseBody { get; set; }
    }

    public class ApplicationLog
    {
        public Guid CorrelationId { get; set; }

        public LogType LogType { get; set; }

        public DateTime Timestamp { get; set; }

        public string ExceptionLevel { get; set; }

        public string Exception { get; set; }

        public object log { get; set; }
    }
}
