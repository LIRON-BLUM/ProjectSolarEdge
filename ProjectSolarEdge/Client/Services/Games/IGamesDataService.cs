using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Client.Services.Games
{
    public interface IGamesDataService
    {
    
        Task<IEnumerable<Game>> GetGames();
        Task<IEnumerable<Game>> GetAllGames();

        Task<Game> GetGameByIdAsync(int Id);

        Task<bool> UpdateGame(Game game);

        Task<int> AddGameToDB(Game game);


        Task<bool> DeleteQuestionConnection(int Id);

        Task<bool> DeleteQuestionIDConnction(int QuestionID, int GameID);

        Task<int> AddQuestionConnection(GameQuestionsConnection gameQuestionsConnection);
    }
}
