using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSolarEdge.Shared.Services.Users
{
    public static class UsersQueries
    {

        public static string GetAllUsers => @"SELECT * FROM UsersTable WHERE isDeleted = 0";

        public static string GetUserByID => @"SELECT * FROM UsersTable WHERE ID = @ID AND isDeleted = 0";

        public static string GetUserByUserName => @"SELECT * FROM UsersTable WHERE UserName = @UserName";

        public static string GetUserByPassword => @"SELECT * FROM UsersTable WHERE UserPassword = @UserPassword";
    }
}
