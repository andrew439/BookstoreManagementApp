using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IEmployeeManager
    {
        List<EmployeeVM> RetrieveAllActiveEmployeeVMs();

        EmployeeVM RetrieveEmployeeByEmployeeID(int employeeID);

        bool EditEmployee(EmployeeVM oldEmployee, EmployeeVM newEmployee);
    }
}
