using Northwind.Repository.Common.Helper;
using Northwind.Repository.Common.Interface;
using Northwind.Repository.DTO;
using Northwind.Repository.Repository;
using NUnit.Framework;
using System.Collections;
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

        [Test]
        [TestCaseSource("InsertNewCustomerTestData")]
        public void TestInsertNewCustomer(CustomersDto customersDto)
        {
            IDatabaseConnectionHelper databaseConnectionHelper = new NorthwindDbConnectionHelper();
            CustomersRepository customersRepository = new CustomersRepository(databaseConnectionHelper);
            var result = customersRepository.InsertNewCustomer(customersDto);
            Assert.True(result);
        }

        [Test]
        [TestCaseSource("UpdateCustomerTestData")]
        public void TestUpdateCustomer(CustomersDto customersDto)
        {
            IDatabaseConnectionHelper databaseConnectionHelper = new NorthwindDbConnectionHelper();
            CustomersRepository customersRepository = new CustomersRepository(databaseConnectionHelper);
            var result = customersRepository.UpdateCustomer(customersDto);
            Assert.True(result);
        }

        public static IEnumerable InsertNewCustomerTestData
        {
            get
            {
                yield return new TestCaseData(new CustomersDto() {
                    CustomerID = "AAAAA",
                    CompanyName = "NewCompany",
                    ContactName = "JIASYUAN",
                    ContactTitle = "Owner/Marketing Assistant",
                    Address = "Keskuskatu 45",
                    City = "Keelung",
                    Region = "Qidu ",
                    PostalCode = "20649",
                    Country = "TW",
                    Phone = "24000000",
                    Fax = "24000001"
                });
            }
        }

        public static IEnumerable UpdateCustomerTestData
        {
            get
            {
                yield return new TestCaseData(new CustomersDto()
                {
                    CustomerID = "AAAAA",
                    CompanyName = "UpdateCompany",
                    ContactName = "Dirk",
                    ContactTitle = "Owner/Marketing Assistant",
                    Address = "Forsterstr. 57 45",
                    City = "Taipei",
                    Region = "Da'an ",
                    PostalCode = "10649",
                    Country = "TW",
                    Phone = "2700000",
                    Fax = "27000001"
                });
            }
        }
    }

}
