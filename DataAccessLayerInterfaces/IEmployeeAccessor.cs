using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface IEmployeeAccessor
    {
        List<EmployeeVM> SelectAllActiveEmployees();

        EmployeeVM SelectEmployeeByEmployeeID(int employeeID);

        int UpdateEmployee(EmployeeVM oldEmployee, EmployeeVM newEmployee);
    }
}
