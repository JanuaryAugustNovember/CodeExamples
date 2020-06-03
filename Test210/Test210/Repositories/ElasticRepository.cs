using Elasticsearch.Net;
using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test210.Infrastructure;
using Test210.Logging;

namespace Test210.Repositories
{
    public class ElasticRepository : IElasticRepository
    {
        private readonly AppSettings _appSettings;
        private ConnectionSettings connectionSettings;

        public ElasticRepository(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;

            connectionSettings = new ConnectionSettings(new Uri(_appSettings.ElasticSearchUri));
        }

        public void SaveApiLog(ApiLog log)
        {
            connectionSettings.DefaultIndex("apilog");

            var elasticClient = new ElasticClient(connectionSettings);

            var response = elasticClient.IndexDocument(log);
        }

        public void SaveApplicationLog(ApplicationLog log)
        {
            connectionSettings.DefaultIndex("applog");

            var elasticClient = new ElasticClient(connectionSettings);

            var response = elasticClient.IndexDocument(log);
        }

        public IEnumerable<ApiLog> SearchApiLogsById(string correlationId)
        {
            connectionSettings.DefaultIndex("apilog");

            var elasticClient = new ElasticClient(connectionSettings);

            var apiLogs = elasticClient.Search<ApiLog>(s => s
                .From(0)
                .Size(10)
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.CorrelationId)
                        .Query(correlationId)
                    )
                )
            ).Documents;

            return apiLogs;
        }
    }
}
