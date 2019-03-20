using Northwind.Repository.Enum;
using System;

namespace Northwind.Repository.Common
{
    public class ConnectionStringFactory
    {
        private string _connectionString=string.Empty;
        public string GetConnectionString(DataBaseEnum dataBase)
        {
            switch (dataBase)
            {
                case DataBaseEnum.Northwind:
                    this._connectionString = ConnectionString.NorthwindConnectionString;
                    break;
               //TODO:Add other case
            }
            if (string.IsNullOrWhiteSpace(_connectionString))
            {
                throw new ArgumentNullException(nameof(_connectionString));
            }

            return _connectionString;
        }
    }
}
