using Dapper;
using Northwind.Repository.Common.Interface;
using Northwind.Repository.DTO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;

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

      
        public bool InsertNewCustomer(CustomersDto customersDto)
        {
            bool result = false;
            string sqlCommand = @"INSERT INTO [dbo].[Customers]
                                       ([CustomerID]
                                       ,[CompanyName]
                                       ,[ContactName]
                                       ,[ContactTitle]
                                       ,[Address]
                                       ,[City]
                                       ,[Region]
                                       ,[PostalCode]
                                       ,[Country]
                                       ,[Phone]
                                       ,[Fax])
                                 VALUES
                                       (@CustomerID
                                       ,@CompanyName
                                       ,@ContactName
                                       ,@ContactTitle
                                       ,@Address
                                       ,@City
                                       ,@Region
                                       ,@PostalCode
                                       ,@Country
                                       ,@Phone
                                       ,@Fax
		                               )";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("CustomerID", customersDto.CustomerID, DbType.String);
            dynamicParams.Add("CompanyName", customersDto.CompanyName, DbType.String);
            dynamicParams.Add("ContactName", customersDto.ContactName, DbType.String);
            dynamicParams.Add("ContactTitle", customersDto.ContactTitle, DbType.String);
            dynamicParams.Add("Address", customersDto.Address, DbType.String);
            dynamicParams.Add("City", customersDto.City, DbType.String);
            dynamicParams.Add("Region", customersDto.Region, DbType.String);
            dynamicParams.Add("PostalCode", customersDto.PostalCode, DbType.String);
            dynamicParams.Add("Country", customersDto.Country, DbType.String);
            dynamicParams.Add("Phone", customersDto.Phone, DbType.String);
            dynamicParams.Add("Fax", customersDto.Fax, DbType.String);
            var dbConnection = this.DatabaseConnection.Create();
            using (TransactionScope scope = new TransactionScope())
            {
                using (var conn = dbConnection)
                {
                    result = conn.Execute(sqlCommand, dynamicParams) > 0;
                }
                scope.Complete();
            }
            return result;
        }
    }
}
