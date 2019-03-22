using Northwind.Repository.Common.Helper;
using Northwind.Repository.Common.Interface;
using Northwind.Repository.Repository;
using NUnit.Framework;
using System.Linq;

namespace Northwind.UnitTest.Repository
{
    [TestFixture()]
    public class CustomersRepositoryUnitTest
    {
       [Test]
        public void TestGetAllCustomers()
        {
            IDatabaseConnectionHelper databaseConnectionHelper = new NorthwindDbConnectionHelper();
            CustomersRepository customersRepository = new CustomersRepository(databaseConnectionHelper);
            var result = customersRepository.GetAll();
            Assert.True((result != null && result.Count() != 0));
          
        }

        [Test]
        [TestCase("WOLZA")]
        [TestCase("WILMK")]
        public void TestGetCustomerByPK(string customerID)
        {
            IDatabaseConnectionHelper databaseConnectionHelper = new NorthwindDbConnectionHelper();
            CustomersRepository customersRepository = new CustomersRepository(databaseConnectionHelper);
            var result = customersRepository.GetCustomer(customerID);
            Assert.True((result != null && result.CustomerID == customerID));
        }

    }
}
