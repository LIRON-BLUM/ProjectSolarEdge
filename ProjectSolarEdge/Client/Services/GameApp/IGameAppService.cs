using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Client.Services.GameApp
{
    public interface IGameAppService
    {
        Task<UsersGameRecord> GetUsersGameRecordById(int Id);

    }
}
