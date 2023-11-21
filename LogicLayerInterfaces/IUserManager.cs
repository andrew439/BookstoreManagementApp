using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IUserManager
    {
        // this method should return a user or throw
        // a bad argument exception
        User LoginUser(string email, string password);
        string HashSha256(string source);
        bool ResetPassword(User user, string email, string password,
            string oldPassword);
        User AuthenticateUser(string email, string password);
        List<User> RetrieveUserListByActive(bool active = true);
        bool EditUser(User oldUser, User newUser);
        bool AddUser(User user);
        bool SetUserActiveState(bool active, int employeeID);
        List<string> RetrieveEmployeeRoles(int employeeID);
        List<string> RetrieveEmployeeRoles();
        bool DeleteUserRole(int employeeID, string role);
        bool AddUserRole(int employeeID, string role);
        bool FindUser(string email);
        int RetrieveUserIDFromEmail(string email);

    }
}
