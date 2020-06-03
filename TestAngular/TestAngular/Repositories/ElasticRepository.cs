using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAngular.Infrastructure;

namespace TestAngular.Repositories
{
    public class ElasticRepository : IElasticRepository
    {
        private ConnectionSettings _connectionSettings;

        public ElasticRepository()
        {
            _connectionSettings = new ConnectionSettings(new Uri("http://localhost:9200"));
        }

        public bool IsElasticRunning()
        {
            var elasticClient = new ElasticClient(_connectionSettings);

            var status = elasticClient.Ping();

            if (status.IsValid)
                return true;
            else
                return false;
        }
    }
}
