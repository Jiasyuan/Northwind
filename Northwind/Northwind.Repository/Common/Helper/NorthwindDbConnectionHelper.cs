using Northwind.Repository.Common.Interface;
using Northwind.Repository.Enum;
using System.Data;
using System.Data.SqlClient;

namespace Northwind.Repository.Common.Helper
{
    public class NorthwindDbConnectionHelper : IDatabaseConnectionHelper
    {
        private readonly string _connectionString;

        public NorthwindDbConnectionHelper(DataBaseEnum dataBase)
        {
            this._connectionString = new ConnectionStringFactory().GetConnectionString(dataBase);
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
