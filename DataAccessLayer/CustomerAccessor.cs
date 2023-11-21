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
    public class CustomerAccessor : ICustomerAccessor
    {
        public List<Customer> SelectAllCustomers()
        {
            List<Customer> customers = new List<Customer>();

            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_all_active_customers";

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

                        var customer = new Customer();
                        customer.CustomerID = reader.GetInt32(0);
                        customer.GivenName = reader.GetString(1);
                        customer.FamilyName = reader.GetString(2);
                        customer.Phone = reader.GetString(3);
                        customer.Email = reader.GetString(4);
                        customer.OkToContact = reader.GetBoolean(5);
                        customer.Active = reader.GetBoolean(6);

                        customers.Add(customer);
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
            return customers;
        }


        public List<Customer> SelectAllActiveCustomers()
        {
            List<Customer> customers = new List<Customer>();

            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_all_active_customers";

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
                     
                        var customer = new Customer();
                        customer.CustomerID = reader.GetInt32(0);
                        customer.GivenName = reader.GetString(1);
                        customer.FamilyName = reader.GetString(2);
                        customer.Phone = reader.GetString(3);
                        customer.Email = reader.GetString(4);
                        customer.OkToContact = reader.GetBoolean(5);
                        customer.Active = reader.GetBoolean(6);

                        customers.Add(customer);
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
            return customers;
        }

    }
}
