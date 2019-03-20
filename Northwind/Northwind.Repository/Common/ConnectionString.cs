using System.Configuration;

namespace Northwind.Repository.Common
{
    public static class ConnectionString
    {
        public static string NorthwindConnectionString => ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString;
    }
}
