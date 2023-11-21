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
    public class EmployeeManager : IEmployeeManager
    {
        private IEmployeeAccessor _employeeAccessor = null;

        public EmployeeManager()
        {
            _employeeAccessor = new DataAccessLayer.EmployeeAccessor();
        }

        public EmployeeManager(IEmployeeAccessor employeeAccessor)
        {
            _employeeAccessor = employeeAccessor;
        }

        public bool EditEmployee(EmployeeVM oldEmployee, EmployeeVM newEmployee)
        {
            bool success = false;
            if (1 == _employeeAccessor.UpdateEmployee(oldEmployee, newEmployee))
            {
                success = true;
            }
            return success;
        }

        public List<EmployeeVM> RetrieveAllActiveEmployeeVMs()
        {
            /*
                var employees = new List<EmployeeVM>();
                employees.Add(new EmployeeVM());
                employees.Add(new EmployeeVM());
                employees.Add(new EmployeeVM());
            */

            List<EmployeeVM> employees = null;
            try
            {
                employees = _employeeAccessor.SelectAllActiveEmployees();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Data not found.", ex);
            }

            return employees;
        }

        public EmployeeVM RetrieveEmployeeByEmployeeID(int employeeID)
        {
            try
            {
                return _employeeAccessor.SelectEmployeeByEmployeeID(employeeID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found.", ex);
            }
        }
    }
}
