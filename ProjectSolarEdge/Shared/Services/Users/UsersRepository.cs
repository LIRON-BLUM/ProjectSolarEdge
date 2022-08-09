using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSolarEdge.Shared.Data;
using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Shared.Services.Users
{
    public class UsersRepository : DbRepository, IUsersRepository
    {
        public UsersRepository(string connectionString) : base(connectionString)
        {
        }

        public IEnumerable<UsersTable> GetAllUsers()
        {
            IEnumerable<UsersTable> _users = GetRecords<UsersTable>(UsersQueries.GetAllUsers, null);
            return _users;
        }

        public UsersTable GetUserByID(int userID)
        {
            UsersTable _userID = GetRecords<UsersTable>(UsersQueries.GetUserByID, new { ID = userID }).FirstOrDefault();
            return _userID;
        }

        public UsersTable GetUserIdByUserName(string userName)
        {
            UsersTable _userID = GetRecords<UsersTable>(UsersQueries.GetUserByUserName, new { UserName = userName }).FirstOrDefault();
            return _userID;
        }

        public UsersTable GetUserIdByUserPassword(string userPassword)
        {
            UsersTable _userID = GetRecords<UsersTable>(UsersQueries.GetUserByPassword, new { UserPassword = userPassword }).FirstOrDefault();
            return _userID;
        }
    }
}
