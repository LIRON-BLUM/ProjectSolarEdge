using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Client.Services.Users
{
    public interface IUsersDataService
    {

        Task<UsersTable> GetUsererByID(int UserID);

        Task<UsersTable> GetUserIdByUserName(string UserName);

        Task<UsersTable> GetUserIdByUserPassword(string UserPassword);
    }
}
