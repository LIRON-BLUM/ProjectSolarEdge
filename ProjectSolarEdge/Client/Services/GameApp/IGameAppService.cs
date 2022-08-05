using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Client.Services.GameApp
{
    public interface IGameAppService
    {
        //Task<UsersGameRecord> GetUsersGameRecordById(int Id);

        //Task<IEnumerable<UsersGameRecord>> GetUsersGameRecordByGameId(int gameId);

        //Task<UsersGameRecord> GetUsersGameRecord();

        Task<IEnumerable<UserGameScore>> GetGameUsersScore(int gameId);
        Task<UserGameScore> GetGameUserScoreByUserID(int GameID, int UserID);

       


        Task<UsersTable> GetPlayerByID(int UserID);

        Task<IEnumerable<Question>> AvailableQuestions(int GameID, int UserID);

        Task<IEnumerable<GameScore>> GetAllUserGameScore(int GameID, int UserID);

        Task<int> AddScoreElement(GameScore gameScore);

        Task<bool> UpdateScoreElement(GameScore gameScore);

        Task<GameQuestionsConnection> GetQuestionScoreByGameID(int GameID, int QuestionID);

        Task<GameScore> GetGamblingScore(int GameID, int UserID);

    }
}
