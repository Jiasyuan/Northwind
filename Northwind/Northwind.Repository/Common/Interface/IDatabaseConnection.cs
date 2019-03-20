using System.Data;

namespace Northwind.Repository.Common.Interface
{
    public interface IDatabaseConnection
    {
        IDbConnection Create();
    }
}
