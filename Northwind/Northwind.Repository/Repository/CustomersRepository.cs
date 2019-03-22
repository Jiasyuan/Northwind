using Dapper;
using Northwind.Repository.Common.Interface;
using Northwind.Repository.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;

namespace Northwind.Repository.Repository
{
    public class CustomersRepository : IDisposable
    {
        private IDatabaseConnectionHelper DatabaseConnection { get; }

        public CustomersRepository(IDatabaseConnectionHelper databaseConnectionHelper)
        {
            this.DatabaseConnection = databaseConnectionHelper;
        }

        #region IDisposable Support
        private bool disposedValue = false; 

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    return;
                }
                disposedValue = true;
            }
        }
        ~CustomersRepository()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

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
                var result = conn.Query<CustomersDto>(sqlCommand, new { customerID }).FirstOrDefault();
                return result;
            }
        }

        /// <summary>
        /// Insert New Customer
        /// </summary>
        /// <param name="customersDto"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Update Customer
        /// </summary>
        /// <param name="customersDto"></param>
        /// <returns></returns>
        public bool UpdateCustomer(CustomersDto customersDto)
        {
            bool result = false;
            string sqlCommand = @"UPDATE [dbo].[Customers]
                                                        SET [CompanyName] = @CompanyName
                                                          ,[ContactName] = @ContactName
                                                          ,[ContactTitle] = @ContactTitle
                                                          ,[Address] = @Address
                                                          ,[City] = @City
                                                          ,[Region] = @Region
                                                          ,[PostalCode] = @PostalCode
                                                          ,[Country] = @Country
                                                          ,[Phone] = @Phone
                                                          ,[Fax] = @Fax
                                                     WHERE [CustomerID]= @CustomerID";
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


        /// <summary>
        /// Delete Customer By PK
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public bool DeleteCustomer(string customerID)
        {
            bool result = false;
            string sqlCommand = @"DELETE FROM [dbo].[Customers]
                                                     WHERE [CustomerID]= @customerID";

            var dbConnection = this.DatabaseConnection.Create();
            using (TransactionScope scope = new TransactionScope())
            {
                using (var conn = dbConnection)
                {
                    result = conn.Execute(sqlCommand, new { customerID }) > 0;
                }
                scope.Complete();
            }
            return result;
        }
    }
}
