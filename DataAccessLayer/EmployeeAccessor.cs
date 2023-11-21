using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayer
{
    public class EmployeeAccessor : IEmployeeAccessor
    {
        public int UpdateEmployee(EmployeeVM oldEmployee, EmployeeVM newEmployee)
        {
            throw new NotImplementedException();
        }

        public List<EmployeeVM> SelectAllActiveEmployees()
        {
            List<EmployeeVM> employees = new List<EmployeeVM>();

            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_all_active_employees";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // try-catch-finally
            try
            {
                // open a connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();

                // process the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        /*
                         *  [Employee].[EmployeeID], [GivenName], [FamilyName], [Phone], [Email], [ClockedIn],
				         *  [AddressLine1], [AddressLine2], [City], [State], [Address].[ZIP]
                         */
                        var employee = new EmployeeVM();
                        employee.EmployeeID = reader.GetInt32(0);
                        employee.GivenName = reader.GetString(1);
                        employee.FamilyName = reader.GetString(2);
                        employee.Phone = reader.GetString(3);
                        employee.Email = reader.GetString(4);
                        employee.ClockedIn = reader.GetBoolean(5);
                        employee.Active = reader.GetBoolean(6);
                        employee.AddressLine1 = reader.GetString(7);
                        employee.AddressLine2 = reader.GetValue(8).ToString();
                        employee.City = reader.GetString(9);
                        employee.State = reader.GetString(10);
                        employee.ZIPCode = reader.GetString(11);

                        employees.Add(employee);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return employees;
        }

        public EmployeeVM SelectEmployeeByEmployeeID(int employeeID)
        {
            EmployeeVM employee = new EmployeeVM();

            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_employee_by_employeeID";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

            // try-catch-finally
            try
            {
                // open a connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();

                // process the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        employee.EmployeeID = reader.GetInt32(0);
                        employee.GivenName = reader.GetString(1);
                        employee.FamilyName = reader.GetString(2);
                        employee.Phone = reader.GetString(3);
                        employee.Email = reader.GetString(4);
                        employee.ClockedIn = reader.GetBoolean(5);
                        employee.Active = reader.GetBoolean(6);
                        employee.AddressLine1 = reader.GetString(7);
                        employee.AddressLine2 = reader.GetValue(8).ToString();
                        employee.City = reader.GetString(9);
                        employee.State = reader.GetString(10);
                        employee.ZIPCode = reader.GetString(11);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return employee;
        }
    }
}
