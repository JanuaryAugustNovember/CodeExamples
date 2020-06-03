
using Dapper;
using Npgsql;

using System.Collections.Generic;
using System.Linq;

using TestAngular.Domain;
using TestAngular.Infrastructure;

namespace TestAngular.Repositories
{
    public class DealerGridRepository : IDealerGridRepository
    {
        public List<DealerDataSource> GetConnectionStrings(string searchText)
        {
            var dataSources = new List<DealerDataSource>();

            using (var conn = new NpgsqlConnection("Host=10.57.4.122;Port=6432;Username=lyadmin;Password=Kc56qhVza>jvu{jzRT8594^[;Database=dealergrid;Maximum Pool Size=700"))
            {
                var query = string.Format("select dealerdatasourceid as DealerDataSourceId, pghostaddress::varchar as HostAddress, pghostport as HostPort, pgdatabasename as Database, isenabled as IsEnabled, dealernumber as DealerNumber, dealerenvironmentid as DealerEnvironmentId, pgusername as Username, pgpassword as password from dealerdatasources where pgdatabasename like '%{0}%'", searchText);

                dataSources = conn.Query<DealerDataSource>(query, searchText).ToList();
            }

            return dataSources;
        }
    }
}
