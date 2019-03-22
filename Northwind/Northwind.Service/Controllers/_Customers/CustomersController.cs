using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Northwind.Contract.Model;
using Northwind.Repository.;

namespace Northwind.Service.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomersController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Customer> GetAllCustomers()
        {
            return null;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}