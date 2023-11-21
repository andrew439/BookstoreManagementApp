using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayerFakes
{
    public class CustomerAccessorFakes : ICustomerAccessor
    {
        private List<Customer> fakeCustomers = new List<Customer>();

        public CustomerAccessorFakes()
        {
            fakeCustomers.Add(new Customer
            {
                CustomerID = 999999,
                Email = "tess@customer.com",
                GivenName = "Tess",
                FamilyName = "User",
                Phone = "1234567890",
                OkToContact = false,
                Active = false
            });

            fakeCustomers.Add(new Customer
            {
                CustomerID = 999998,
                Email = "fake2@customer.com",
                GivenName = "Tess",
                FamilyName = "User",
                Phone = "1234567890",
                OkToContact = true,
                Active = true
            });
        }

        public List<Customer> SelectAllActiveCustomers()
        {
            return fakeCustomers.Where(c => c.Active == true).ToList();
        }

        public List<Customer> SelectAllCustomers()
        {
            return fakeCustomers;
        }
    }
}
