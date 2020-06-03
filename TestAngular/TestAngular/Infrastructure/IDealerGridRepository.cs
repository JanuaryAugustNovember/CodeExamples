
using System.Collections.Generic;

using TestAngular.Domain;

namespace TestAngular.Infrastructure
{
    public interface IDealerGridRepository
    {
        List<DealerDataSource> GetConnectionStrings(string searchText);
    }
}
