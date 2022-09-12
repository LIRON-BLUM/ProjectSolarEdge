using ProjectSolarEdge.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSolarEdge.Shared.Services.Users
{
    public interface IUsersRepository
    {
        IEnumerable<UsersTable> GetAllUsers();
        UsersTable GetUserByID(int userID);
        UsersTable GetUserIdByUserName(string userName);

        UsersTable GetUserIdByUserPassword(string userPassword);

        int AddUser(UsersTable user);

        bool UpdateUser(UsersTable user);
        bool DeleteUser(UsersTable user);
    }
}
