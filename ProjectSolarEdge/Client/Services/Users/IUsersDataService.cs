using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Client.Services.Users
{
    public interface IUsersDataService
    {
        
        Task<IEnumerable<UsersTable>> GetAllUsers();

        Task<UsersTable> GetUsererByID(int UserID);

        Task<UsersTable> GetUserIdByUserName(string UserName);

        Task<UsersTable> GetUserIdByUserPassword(string UserPassword);

        Task<int> AddUserToDB(UsersTable newUser);

        // Task<bool> AddQuestionToDB(Question newQuestion);

        Task<bool> UpdateUser(UsersTable user);
        Task<bool> DeleteUser(UsersTable user);
    }
}
