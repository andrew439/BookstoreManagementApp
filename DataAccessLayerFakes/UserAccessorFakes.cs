using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;

namespace DataAccessLayerFakes
{
    public class UserAccessorFakes : IUserAccessor
    {
        private List<User> fakeUsers = new List<User>();
        private List<String> fakePasswordHashes = new List<string>();

        public UserAccessorFakes()
        {
            fakeUsers.Add(new User()
            {
                EmployeeID = 999999,
                Email = "tess@company.com",
                GivenName = "Tess",
                FamilyName = "User",
                Phone = "1234567890",
                ClockedIn = true,
                Active = true,
                Roles = new List<string>()
            }); ;
            fakeUsers[0].Roles.Add("Administrator");
            fakeUsers[0].Roles.Add("Manager");

            fakeUsers.Add(new User()
            {
                EmployeeID = 999998,
                Email = "fake2@company.com",
                GivenName = "Tess",
                FamilyName = "User",
                Phone = "1234567890",
                ClockedIn = false,
                Active = true,
                Roles = new List<string>()
            });

            fakePasswordHashes.Add("9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e");
            fakePasswordHashes.Add("bad hash");
            fakePasswordHashes.Add("bad hash");
        }

        public int ActivateEmployee(int employeeID)
        {
            throw new NotImplementedException();
        }

        public User AuthenticateUser(string username, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash)
        {
            int numAuthenticated = 0;

            // check for user record in fake data
            for (int i = 0; i < fakeUsers.Count; i++)
            {
                if (fakeUsers[i].Email == email &&
                    fakePasswordHashes[i] == passwordHash &&
                    fakeUsers[i].Active == true)
                {
                    numAuthenticated++;
                }
            }

            return numAuthenticated;
        }

        public int DeactivateEmployee(int employeeID)
        {
            throw new NotImplementedException();
        }

        public User getUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public int InsertEmployee(User user)
        {
            throw new NotImplementedException();
        }

        public int InsertOrDeleteEmployeeRole(int employeeID, string role, bool delete = false)
        {
            throw new NotImplementedException();
        }

        public List<string> SelectAllRoles()
        {
            throw new NotImplementedException();
        }

        public List<string> SelectRolesByEmployeeID(int employeeID)
        {
            List<string> roles = new List<string>();

            foreach (var fakeUser in fakeUsers)
            {
                if (fakeUser.EmployeeID == employeeID)
                {
                    roles = fakeUser.Roles;
                    break;
                }
            }
            return roles;
        }

        public User SelectUserByEmail(string email)
        {
            User user = null;
            foreach (var fakeUser in fakeUsers)
            {
                if (fakeUser.Email == email)
                {
                    user = fakeUser;
                }
            }
            if (user == null)
            {
                throw new ApplicationException("User not found.");
            }

            return user;
        }

        public List<User> SelectUsersByActive(bool active = true)
        {
            throw new NotImplementedException();
        }

        public int UpdateEmployee(User oldUser, User newUser)
        {
            throw new NotImplementedException();
        }

        public int UpdatePasswordHash(int employeeID, string passwordHash, string oldPasswordHash)
        {
            throw new NotImplementedException();
        }
    }
}
