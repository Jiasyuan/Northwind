using NUnit.Framework;
using System.Collections.Generic;
using Northwind.Contract.Help;
using Northwind.Contract.Model;
using Northwind.Contract.Enum;
using System.Collections;
using System.Linq;

namespace Northwind.UnitTest.Service
{
    public class CustomersApiUnitTest
    {
        [Test]
        public void GetTestGetAllCustomersApi()
        {
            var result = ApiHelper.GetApi<IEnumerable<Customer>>(EnumApiServer.Northwind, "Customers", "GetAllCustomers");
            Assert.True((result != null && result.Count() != 0));
        }

        [Test]
        [TestCase("WOLZA")]
        [TestCase("WILMK")]
        public void TestGetCustomerByPKApi(string customerID)
        {

            var result = ApiHelper.GetApi<Customer>(EnumApiServer.Northwind, "Customers", "GetCustomer", customerID);
            Assert.True((result != null && result.CustomerID == customerID));
        }

        [Test,Order(1)]
        [TestCaseSource("InsertNewCustomerTestData")]
        public void TesrCreateNewCustomerApi(Customer customer)
        {
            var result = ApiHelper.PostApi<bool>(EnumApiServer.Northwind, "Customers", "CreateNewCustomer", customer);
            Assert.True(result);
        }


        [Test, Order(2)]
        [TestCaseSource("UpdateCustomerTestData")]
        public void TestUpdateCustomerApi(Customer customer)
        {
            var result = ApiHelper.PostApi<bool>(EnumApiServer.Northwind, "Customers", "UpdateCustomer", customer);
            Assert.True(result);
        }

        [Test, Order(3)]
        [TestCase("AAAAA")]
        public void TestDeleteCustomerByPKApi(string customerID)
        {
            var result = ApiHelper.GetApi<bool>(EnumApiServer.Northwind, "Customers", "DeleteCustomer", customerID);
            Assert.True(result);
        }


        #region Test Data
        public static IEnumerable InsertNewCustomerTestData
        {
            get
            {
                yield return new TestCaseData(new Customer()
                {
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
                yield return new TestCaseData(new Customer()
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
        #endregion
    }
}
