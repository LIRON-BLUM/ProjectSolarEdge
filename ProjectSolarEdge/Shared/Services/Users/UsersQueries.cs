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

        public static string AddNewUser => @"INSERT INTO UsersTable
													(UserFirstName,
													UserLastName,
													UserName,
													UserPassword,
													UserType,
													CreationDate,
													UpdateDate,											
													isDeleted)
											  VALUES (@UserFirstName, @UserLastName, @UserName, @UserPassword, @UserType, @CreationDate, @UpdateDate, 0)
											  SELECT CAST(SCOPE_IDENTITY() as int)";




        public static string UpdateUser => @"UPDATE UsersTable 
												   SET
													UserFirstName = @UserFirstName,
													UserLastName =  @UserLastName,
													UserName =  @UserName,
													UserPassword =  @UserPassword,
													UserType =  @UserType,
													UpdateDate = @UpdateDate
											    	WHERE ID = @ID";



        public static string DeleteUser => @"UPDATE UsersTable 
												SET isDeleted = 1
													WHERE ID = @ID";
    }
}
