using Dapper;
using Northwind.Repository.Common.Interface;
using Northwind.Repository.DTO;
using System.Collections.Generic;

namespace Northwind.Repository.Repository
{
    public class CustomersRepository
    {
        private IDatabaseConnectionHelper DatabaseConnection { get; }

        public CustomersRepository(IDatabaseConnectionHelper databaseConnectionHelper)
        {
            this.DatabaseConnection = databaseConnectionHelper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomersDto> GetAll()
        {
            var dbConnection = this.DatabaseConnection.Create();
            using (var conn = dbConnection)
            {
                var sqlCommand = @"select [CustomerID]
                                                            ,[CompanyName]
                                                            ,[ContactName]
                                                            ,[ContactTitle]
                                                            ,[Address]
                                                            ,[City]
                                                            ,[Region]
                                                            ,[PostalCode]
                                                            ,[Country]
                                                            ,[Phone]
                                                            ,[Fax] from dbo.Customers";
                var result = conn.Query<CustomersDto>(sqlCommand);
                return result;
            }
        }
    }
}
