using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWindowsServiceApi.Logging
{
    public class Log
    {
        public Guid CorrelationId { get; set; }

        public string RequestPath { get; set; }

        public string RequestMethod { get; set; }

        public string RequestBody { get; set; }
    }
}
