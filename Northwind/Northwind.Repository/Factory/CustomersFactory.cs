using Northwind.Repository.Common.Helper;
using Northwind.Repository.Repository;

namespace Northwind.Repository.Factory
{
    public static class CustomersFactory
    {
        private static CustomersRepository _customersRepository => new CustomersRepository(new NorthwindDbConnectionHelper());
        public static CustomersRepository CustomersRepository = _customersRepository;
    }
}
