using ProjectSolarEdge.Shared.Entities;
using System.Text;
using System.Text.Json;

namespace ProjectSolarEdge.Client.Services.GameApp
{
    public class GameAppService : IGameAppService
    {
        private readonly HttpClient _httpClient;

        public GameAppService(HttpClient client)
        {
            this._httpClient = client;
        }

  

        public async Task<IEnumerable<Question>> AvailableQuestions(int GameID, int UserID)
        {
            Stream stream = await _httpClient.GetStreamAsync($"api/GameApps/AvailableQuestions/{GameID}/{UserID}");
            return await JsonSerializer.DeserializeAsync<IEnumerable<Question>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<UsersTable> GetPlayerByID(int ID)
        {
            Stream stream = await _httpClient.GetStreamAsync($"api/GameApps/GetUserByID/{ID}");
            return await JsonSerializer.DeserializeAsync<UsersTable>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }



        public async Task<int> AddScoreElement(GameScore gameScore)
        {
            var gameScoreJson =
                new StringContent(JsonSerializer.Serialize(gameScore), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/GameApps/AddScoreElement", gameScoreJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<int>(await response.Content.ReadAsStreamAsync());
            }

            return 0;
        }

        public async Task<bool> UpdateScoreElement(GameScore gameScore)
        {
            var gameScoreJson =
             new StringContent(JsonSerializer.Serialize(gameScore), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("api/GameApps/UpdateScoreElement", gameScoreJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<bool>(await response.Content.ReadAsStreamAsync());
            }

            return false;
        }

        public async Task<IEnumerable<GameScore>> GetAllUserGameScore(int GameID, int UserID)
        {
            Stream stream = await _httpClient.GetStreamAsync($"api/GameApps/GetAllUserGameScore/{GameID}/{UserID}");
            return await JsonSerializer.DeserializeAsync<IEnumerable<GameScore>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<IEnumerable<UserGameScore>> GetGameUsersScore(int gameId)
        {
            Stream stream = await _httpClient.GetStreamAsync($"api/GameApps/GetUsersScore/{gameId}");
            return await JsonSerializer.DeserializeAsync<IEnumerable<UserGameScore>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<UserGameScore> GetGameUserScoreByUserID(int GameID, int UserID)
        {
            Stream stream = await _httpClient.GetStreamAsync($"api/GameApps/GetGameUsersScoreByUserID/{GameID}/{UserID}");
            return await JsonSerializer.DeserializeAsync<UserGameScore>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<GameQuestionsConnection> GetQuestionScoreByGameID(int GameID, int QuestionID)
        {
            Stream stream = await _httpClient.GetStreamAsync($"api/GameApps/GetQuestionScoreByGameID/{GameID}/{QuestionID}");
            return await JsonSerializer.DeserializeAsync<GameQuestionsConnection>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<GameScore> GetGamblingScore(int GameID, int UserID)
        {
            Stream stream = await _httpClient.GetStreamAsync($"api/GameApps/GetGamblingScore/{GameID}/{UserID}");
            return await JsonSerializer.DeserializeAsync<GameScore>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
