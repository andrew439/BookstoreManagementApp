using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayerFakes
{
    public class EmployeeAccessorFakes : IEmployeeAccessor
    {
        private List<EmployeeVM> fakeEmployees = new List<EmployeeVM>();

        public EmployeeAccessorFakes()
        {
            fakeEmployees.Add(new EmployeeVM
            {
                EmployeeID = 999999,
                Email = "tess@company.com",
                GivenName = "Tess",
                FamilyName = "User",
                Phone = "1234567890",
                ClockedIn = true,
                Active = true,
                Roles = new List<string>(),
                AddressLine1 = "111 Test St.",
                AddressLine2 = null,
                City = "Marion",
                State = "IA",
                ZIPCode = "55555"
            }); ;
            fakeEmployees[0].Roles.Add("Administrator");
            fakeEmployees[0].Roles.Add("Manager");

            fakeEmployees.Add(new EmployeeVM
            {
                EmployeeID = 999998,
                Email = "fake2@company.com",
                GivenName = "Tess",
                FamilyName = "User",
                Phone = "1234567890",
                ClockedIn = false,
                Active = true,
                Roles = new List<string>(),
                AddressLine1 = "112 Test St.",
                AddressLine2 = "Unit B",
                City = "Marion",
                State = "IA",
                ZIPCode = "55555"
            });
        }

        public List<EmployeeVM> SelectAllActiveEmployees()
        {
            return fakeEmployees.Where(e => e.Active == true).ToList();
        }

        public EmployeeVM SelectEmployeeByEmployeeID(int employeeID)
        {
            throw new NotImplementedException();
        }

        public int UpdateEmployee(EmployeeVM oldEmployee, EmployeeVM newEmployee)
        {
            throw new NotImplementedException();
        }
    }
}
