using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class UserAccessor : IUserAccessor
    {
        public int ActivateEmployee(int employeeID)
        {
            int rows = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmd = new SqlCommand("sp_reactivate_employee", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        public User AuthenticateUser(string username, string passwordHash)
        {
            User result = null; // change this to 1 if the user is authenticated

            // first, get a connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // next, we need a command object
            var cmd = new SqlCommand("sp_authenticate_user");
            cmd.Connection = conn;

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters for the procedure
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            // set the values for the parameters
            cmd.Parameters["@Email"].Value = username;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            // now that the command is set up, we can execute it
            try
            {
                // open the connection
                conn.Open();

                // execute the command
                if (1 == Convert.ToInt32(cmd.ExecuteScalar()))
                {
                    // if the command worked correctly, get a user
                    // object
                    result = getUserByEmail(username);
                }
                else
                {
                    throw new ApplicationException("User not found.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash)
        {
            int result = 0;

            // ADO.NET needs a connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // next we need command text
            var cmdText = "sp_authenticate_user";

            // use the command text and connection to create a command object
            var cmd = new SqlCommand(cmdText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameter objects to the command
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            // set the values for the parameter objects
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            // now that the command is set up, we can invoke it
            // in a try-catch block
            try
            {
                // open the connection
                conn.Open();

                // execute the command appropriately, and capture the results
                // you can ExecuteScalar, ExecuteNonQuery, or ExecuteReader
                // depending on whether you expect a single value, an int
                // for rows affected, or rows and columns

                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                // close the connection
                conn.Close();
            }

            return result;
        }

        public int DeactivateEmployee(int employeeID)
        {
            int rows = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmd = new SqlCommand("sp_deactivate_employee", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        public int InsertEmployee(User user)
        {
            int employeeID = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmd = new SqlCommand("sp_insert_employee", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GivenName", user.GivenName);
            cmd.Parameters.AddWithValue("@FamilyName", user.FamilyName);
            cmd.Parameters.AddWithValue("@Phone", user.Phone);
            cmd.Parameters.AddWithValue("@Email", user.Email);

            try
            {
                conn.Open();
                employeeID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return employeeID;
        }

        public int InsertOrDeleteEmployeeRole(int employeeID, string role, bool delete = false)
        {
            int rows = 0;

            string cmdText = delete ? "sp_delete_employee_role" : "sp_insert_employee_role";

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            cmd.Parameters.AddWithValue("@RoleID", role);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        public List<string> SelectAllRoles()
        {
            List<string> roles = new List<string>();

            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command objects
            var cmd = new SqlCommand("sp_select_all_roles");
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                // open connection
                conn.Open();

                // execute the first command

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string role = reader.GetString(0);
                    roles.Add(role);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return roles;
        }

        public List<string> SelectRolesByEmployeeID(int employeeID)
        {
            List<string> roles = new List<string>();

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_roles_by_employeeID";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);

            // values
            cmd.Parameters["@EmployeeID"].Value = employeeID;

            // try-catch-finally
            try
            {
                // open the connection
                conn.Open();

                // execute the command
                var reader = cmd.ExecuteReader();

                // process the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                // close the connection
                conn.Close();
            }

            return roles;
        }

        public User SelectUserByEmail(string email)
        {
            User user = null;

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_employee_by_email";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);

            // value
            cmd.Parameters["@Email"].Value = email;

            // try-catch-finally
            try
            {
                // open
                conn.Open();

                // execute and get a SqlDataReader
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    // most of the time there will be a while loop
                    // here, we don't need it

                    reader.Read();
                    // [EmployeeID], [GivenName], [FamilyName], [Phone], [Email], [Active]

                    user = new User();

                    user.EmployeeID = reader.GetInt32(0);
                    user.GivenName = reader.GetString(1);
                    user.FamilyName = reader.GetString(2);
                    user.Phone = reader.GetString(3);
                    user.Email = reader.GetString(4);
                    user.ClockedIn = reader.GetBoolean(5);
                    user.Active = reader.GetBoolean(6);
                }
                // close the reader
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // close
                conn.Close();
            }

            return user;
        }

        public List<User> SelectUsersByActive(bool active = true)
        {
            List<User> users = new List<User>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmd = new SqlCommand("sp_select_users_by_active");
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Active", SqlDbType.Bit);
            cmd.Parameters["@Active"].Value = active;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var user = new User();
                        user.EmployeeID = reader.GetInt32(0);
                        user.GivenName = reader.GetString(1);
                        user.FamilyName = reader.GetString(2);
                        user.Phone = reader.GetString(3);
                        user.Email = reader.GetString(4);
                        user.Active = reader.GetBoolean(5);
                        users.Add(user);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return users;
        }

        public int UpdateEmployee(User oldUser, User newUser)
        {
            int rows = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmd = new SqlCommand("sp_update_employee", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeID", oldUser.EmployeeID);

            cmd.Parameters.AddWithValue("@NewGivenName", newUser.GivenName);
            cmd.Parameters.AddWithValue("@NewFamilyName", newUser.FamilyName);
            cmd.Parameters.AddWithValue("@NewPhone", newUser.Phone);
            cmd.Parameters.AddWithValue("@NewEmail", newUser.Email);

            cmd.Parameters.AddWithValue("@OldGivenName", oldUser.GivenName);
            cmd.Parameters.AddWithValue("@OldFamilyName", oldUser.FamilyName);
            cmd.Parameters.AddWithValue("@OldPhone", oldUser.Phone);
            cmd.Parameters.AddWithValue("@OldEmail", oldUser.Email);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        public int UpdatePasswordHash(int employeeID, string passwordHash, string oldPasswordHash)
        {
            int rows = 0;

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // cmdText
            var cmdText = "sp_update_passwordHash";

            //command
            var cmd = new SqlCommand(cmdText, conn);

            // type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
            cmd.Parameters.AddWithValue("@OldPasswordHash", oldPasswordHash);

            try
            {
                conn.Open();

                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        public User getUserByEmail(string email)
        {
            User user = null;

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command objects (2)
            var cmd1 = new SqlCommand("sp_select_employee_by_email");
            var cmd2 = new SqlCommand("sp_select_roles_by_employeeID");

            cmd1.Connection = conn;
            cmd2.Connection = conn;

            cmd1.CommandType = CommandType.StoredProcedure;
            cmd2.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd1.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd1.Parameters["@Email"].Value = email;

            cmd2.Parameters.Add("@EmployeeID", SqlDbType.Int);
            // we cannot set the value of this parameter yet

            try
            {
                // open connection
                conn.Open();

                // execute the first command
                var reader1 = cmd1.ExecuteReader();

                if (reader1.Read())
                {
                    user = new User();

                    user.EmployeeID = reader1.GetInt32(0);
                    user.GivenName = reader1.GetString(1);
                    user.FamilyName = reader1.GetString(2);
                    user.Phone = reader1.GetString(3);
                    user.ClockedIn = reader1.GetBoolean(4);
                    user.Active = reader1.GetBoolean(5);
                    user.Email = email;
                }
                else
                {
                    throw new ApplicationException("User not found.");
                }
                reader1.Close(); // this is no longer needed

                cmd2.Parameters["@EmployeeID"].Value = user.EmployeeID;
                var reader2 = cmd2.ExecuteReader();

                List<string> roles = new List<string>();
                while (reader2.Read())
                {
                    string role = reader2.GetString(0);
                    roles.Add(role);
                }
                user.Roles = roles;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            return user;
        }
    }
}
