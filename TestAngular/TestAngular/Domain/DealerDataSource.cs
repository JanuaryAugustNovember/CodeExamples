using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAngular.Domain
{
    public class DealerDataSource
    {
        public int DealerDataSourceId { get; set; }

        public string HostAddress { get; set; }

        public int HostPort { get; set; }

        public string Database { get; set; }

        public bool IsEnabled { get; set; }

        public int DealerNumber { get; set; }

        public int DealerEnvironmentId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string DecryptedPassword { get; set; }
    }
}
