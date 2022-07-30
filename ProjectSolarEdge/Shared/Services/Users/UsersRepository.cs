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

        public UsersTable GetUserIdByUserName(string userName)
        {
            UsersTable _userID = GetRecords<UsersTable>(UsersQueries.GetUserByUserName, new { userName = userName }).FirstOrDefault();
            return _userID;
        }
    }
}
