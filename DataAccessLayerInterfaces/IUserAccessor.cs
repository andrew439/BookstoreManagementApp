using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface IUserAccessor
    {
        int AuthenticateUserWithEmailAndPasswordHash(
            string email, string passwordHash);
        User AuthenticateUser(string username, string passwordHash);
        User SelectUserByEmail(string email);
        int UpdatePasswordHash(int employeeID, string passwordHash,
            string oldPasswordHash);
        List<User> SelectUsersByActive(bool active = true);
        int UpdateEmployee(User oldUser, User newUser);
        int InsertEmployee(User user);
        int ActivateEmployee(int employeeID);
        int DeactivateEmployee(int employeeID);
        List<string> SelectAllRoles();
        List<string> SelectRolesByEmployeeID(int employeeID);
        int InsertOrDeleteEmployeeRole(int employeeID, string role, bool delete = false);
        User getUserByEmail(string email);
    }
}
