using Northwind.Contract.Model;
using Northwind.Repository.DTO;
using Northwind.Repository.Factory;
using Northwind.Service.Common;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Northwind.Service.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomersController : ApiController
    {
        /// <summary>
        /// Get All Customers
        /// </summary>
        /// <returns></returns>
        [HttpGet, ActionName("GetAllCustomers")]
        public IEnumerable<Customer> GetAllCustomers()
        {
            try
            {
                var queryResult = CustomersFactory.CustomersRepository.GetAll();
                var result = MapperHelper.MapperProperties<CustomersDto, Customer>(queryResult);
                return result;
            }
            catch (Exception ex)
            {
                ////TODO:write log
                return null;
            }
        }

        /// <summary>
        /// Get Customer
        /// </summary>
        /// <param name="customerID">PK</param>
        /// <returns></returns>
        [HttpGet, ActionName("GetCustomer")]
        public Customer GetCustomer([FromUri(Name ="id")]string customerID)
        {
            try
            {
                var queryResult = CustomersFactory.CustomersRepository.GetCustomer(customerID);
                var result = MapperHelper.MapperProperties<CustomersDto, Customer>(queryResult);
                return result;
            }
            catch (Exception ex)
            {
                //TODO:write log
                return null;
            }
            
        }

        /// <summary>
        /// Create New Customer
        /// </summary>
        /// <param name="customerData"></param>
        [HttpPost, ActionName("CreateNewCustomer")]
        public bool CreateNewCustomer([FromBody]Customer customerData)
        {
            bool issuccess = false;
            try
            {
                issuccess = CustomersFactory.CustomersRepository.InsertNewCustomer(MapperHelper.MapperProperties<Customer, CustomersDto>(customerData));
            }
            catch(Exception ex)
            {
                //TODO:write log
            }
            return issuccess;
        }

        /// <summary>
        /// UpdateCustomer
        /// </summary>
        /// <param name="customerData"></param>
        [HttpPost, ActionName("UpdateCustomer")]
        public bool UpdateCustomer(Customer customerData)
        {
            bool issuccess = false;
            try
            {
                issuccess = CustomersFactory.CustomersRepository.UpdateCustomer(MapperHelper.MapperProperties<Customer, CustomersDto>(customerData));
            }
            catch (Exception ex)
            {
                //TODO:write log
            }
            return issuccess;
        }

        /// <summary>
        /// Delete Customer
        /// </summary>
        /// <param name="customerID">PK</param>
        [HttpGet, ActionName("DeleteCustomer")]
        public bool DeleteCustomer([FromUri(Name ="id")]string customerID)
        {
            bool issuccess = false;
            try
            {
                issuccess = CustomersFactory.CustomersRepository.DeleteCustomer(customerID);
            }
            catch (Exception ex)
            {
                //TODO:write log
            }
            return issuccess;
        }
    }
}