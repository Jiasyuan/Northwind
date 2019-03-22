using Dapper;
using Northwind.Repository.Common.Interface;
using Northwind.Repository.DTO;
using System.Collections.Generic;
using System.Linq;

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
        /// Get all Customers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomersDto> GetAll()
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
            var dbConnection = this.DatabaseConnection.Create();
            using (var conn = dbConnection)
            {
                var result = conn.Query<CustomersDto>(sqlCommand);
                return result;
            }
        }

        /// <summary>
        /// Get Customer By CustomerID
        /// </summary>
        /// <param name="customerID">PK:CustomerID</param>
        /// <returns></returns>
        public CustomersDto GetCustomer(string customerID)
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
                                                            ,[Fax] from dbo.Customers
                                                            Where [CustomerID]= @customerID";
            var dbConnection = this.DatabaseConnection.Create();
            using (var conn = dbConnection)
            {
                var result = conn.Query<CustomersDto>(sqlCommand,new { customerID }).FirstOrDefault();
                return result;
            }
        }
    }
}
