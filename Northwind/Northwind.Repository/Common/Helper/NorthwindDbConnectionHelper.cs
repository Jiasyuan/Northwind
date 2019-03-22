using Northwind.Repository.Common.Interface;
using Northwind.Repository.Enum;
using System.Data;
using System.Data.SqlClient;

namespace Northwind.Repository.Common.Helper
{
    public class NorthwindDbConnectionHelper : IDatabaseConnectionHelper
    {
        private readonly string _connectionString;

        public NorthwindDbConnectionHelper()
        {
            this._connectionString = new ConnectionStringFactory().GetConnectionString(DataBaseEnum.Northwind);
        }

        /// <summary>
        /// Create DbConnection
        /// </summary>
        /// <returns></returns>
        public IDbConnection Create()
        {
            var sqlConnection = new SqlConnection(_connectionString);
            return sqlConnection;
        }
    }
}
