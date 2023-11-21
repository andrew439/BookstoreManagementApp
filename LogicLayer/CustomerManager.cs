using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;
using LogicLayerInterfaces;

namespace LogicLayer
{
    public class CustomerManager : ICustomerManager
    {
        private ICustomerAccessor _customerAccessor = null;

        public CustomerManager()
        {
            _customerAccessor = new DataAccessLayer.CustomerAccessor();
        }

        public CustomerManager(ICustomerAccessor customerAccessor)
        {
            _customerAccessor = customerAccessor;
        }

        public List<Customer> RetrieveAllCustomers()
        {
            /*
                var customers = new List<Customer>();
                customers.Add(new Customer());
                customers.Add(new Customer());
                return customers;
            */
            List<Customer> customers = null;
            try
            {
                customers = _customerAccessor.SelectAllCustomers();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Data not found.", ex);
            }

            return customers;
        }

        public List<Customer> RetrieveAllActiveCustomers()
        {
            /*
                var customers = new List<Customer>();
                customers.Add(new Customer());
                return customers;
            */
            List<Customer> customers = null;
            try
            {
                customers = _customerAccessor.SelectAllActiveCustomers();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Data not found.", ex);
            }

            return customers;
        }
    }
}
