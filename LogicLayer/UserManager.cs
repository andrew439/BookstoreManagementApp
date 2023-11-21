using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessLayerInterfaces;
using DataAccessLayer;
using System.Security.Cryptography;


namespace LogicLayer
{
    public class UserManager : IUserManager
    {
        private IUserAccessor _userAccessor = null;

        public UserManager()
        {
            _userAccessor = new UserAccessor();
        }

        public UserManager(IUserAccessor ua)
        {
            _userAccessor = ua;
        }

        public bool AddUser(User user)
        {
            bool result = true;
            try
            {
                result = _userAccessor.InsertEmployee(user) > 0;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("User not added", ex);
            }
            return result;
        }

        public bool AddUserRole(int employeeID, string role)
        {
            bool result = false;
            try
            {
                result = 1 == _userAccessor.InsertOrDeleteEmployeeRole(employeeID, role);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Role not added!", ex);
            }
            return result;
        }

        public User AuthenticateUser(string email, string password)
        {
            User result = null;

            // we need to hash the password
            var passwordHash = hashPassword(password);
            password = null;

            try
            {
                result = _userAccessor.AuthenticateUser(email, passwordHash);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Login failed!", ex);
            }

            return result;
        }

        public bool DeleteUserRole(int employeeID, string role)
        {
            bool result = false;
            try
            {
                result = 1 == _userAccessor.InsertOrDeleteEmployeeRole(employeeID, role, delete: true);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Role not removed!", ex);
            }
            return result;
        }

        public bool EditUser(User oldUser, User newUser)
        {
            bool result = false;

            try
            {
                result = _userAccessor.UpdateEmployee(oldUser, newUser) == 1;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update failed", ex);
            }

            return result;
        }

        public string HashSha256(string source)
        {
            string result = "";

            // check for missing input
            if (source == "" || source == null)
            {
                throw new ArgumentNullException("Missing Input");
            }

            // create a byte array
            byte[] data;

            // create a .NET hash provider object
            using (SHA256 sha256hasher = SHA256.Create())
            {
                // hash the input
                data = sha256hasher.ComputeHash(
                    Encoding.UTF8.GetBytes(source));
            }

            // create output with a stringbuilder object
            var s = new StringBuilder();

            // loop through the hashed output making characters
            // from the values in the byte array
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }

            // convert the stringbuilder into a string

            result = s.ToString();
            result = result.ToLower();

            return result;
        }

        public User LoginUser(string email, string password)
        {
            User user = null;

            try
            {
                password = HashSha256(password);
                if (1 == _userAccessor.AuthenticateUserWithEmailAndPasswordHash(
                    email, password))
                {
                    user = _userAccessor.SelectUserByEmail(email);
                    user.Roles = _userAccessor.SelectRolesByEmployeeID(user.EmployeeID);
                }
                else
                {
                    throw new ApplicationException("User not found.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Bad Username or Password", ex);
            }

            return user;
        }

        public bool ResetPassword(User user, string email, string password, string oldPassword)
        {
            bool success = false;
            password = HashSha256(password);
            oldPassword = HashSha256(oldPassword);

            if (user.Email != email)
            {
                success = false;
            }
            else if (1 == _userAccessor.UpdatePasswordHash(user.EmployeeID, password, oldPassword))
            {
                success = true;
            }

            return success;
        }

        public List<string> RetrieveEmployeeRoles(int employeeID)
        {
            List<string> roles = null;

            try
            {
                roles = _userAccessor.SelectRolesByEmployeeID(employeeID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Roles not found", ex);
            }

            return roles;
        }

        public List<string> RetrieveEmployeeRoles()
        {
            List<string> roles = null;

            try
            {
                roles = _userAccessor.SelectAllRoles();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Roles not found", ex);
            }

            return roles;
        }

        public List<User> RetrieveUserListByActive(bool active = true)
        {
            try
            {
                return _userAccessor.SelectUsersByActive(active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found.", ex);
            }
        }

        public bool SetUserActiveState(bool active, int employeeID)
        {
            bool result = false;
            try
            {
                if (active)
                {
                    result = 1 == _userAccessor.ActivateEmployee(employeeID);
                }
                else
                {
                    result = 1 == _userAccessor.DeactivateEmployee(employeeID);
                }
                if (result == false)
                {
                    throw new ApplicationException("Employee record not updated.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update failed!", ex);
            }
            return result;
        }

        private string hashPassword(string source)
        {
            // use SHA256
            string result = null;

            // we need a byte array because cryptography is bits and bytes
            byte[] data;

            // create a has provider object
            using (SHA256 sha256hash = SHA256.Create())
            {
                // hash the input
                data = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(source));
            }
            // build a string from the result
            var s = new StringBuilder();

            // loop through the bytes to build a string
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }

            result = s.ToString().ToUpper();

            return result;
        }

        public bool FindUser(string email)
        {
            try
            {
                return _userAccessor.getUserByEmail(email) != null;
            }
            catch (ApplicationException ex)
            {
                if (ex.Message == "User not found.")
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Database Error", ex);
            }
        }

        public int RetrieveUserIDFromEmail(string email)
        {
            try
            {
                return _userAccessor.getUserByEmail(email).EmployeeID;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Database Error", ex);
            }
        }
    }
}
