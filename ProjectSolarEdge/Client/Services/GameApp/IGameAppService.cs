using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Client.Services.GameApp
{
    public interface IGameAppService
    {
        Task<UsersGameRecord> GetUsersGameRecordById(int Id);

        Task<IEnumerable<UsersGameRecord>> GetUsersGameRecordByGameId(int gameId);

        Task<UsersGameRecord> GetUsersGameRecord();

        //Task<IEnumerable<Subject>> GetSubjectsAsync();

        Task<UsersTable> GetPlayerByID(int UserID);

        Task<IEnumerable<Question>> AvailableQuestions(int GameID, int UserID);

        Task<int> AddScoreElement(GameScore gameScore);

        Task<bool> UpdateScoreElement(GameScore gameScore);

    }
}
