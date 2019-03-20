using System.Data;

namespace Northwind.Repository.Common.Interface
{
    public interface IDatabaseConnectionHelper
    {
        IDbConnection Create();
    }
}
