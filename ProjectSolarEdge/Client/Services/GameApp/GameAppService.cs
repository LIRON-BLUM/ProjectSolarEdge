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

        public async Task<UsersGameRecord> GetUsersGameRecord()
        {
            Stream stream = await _httpClient.GetStreamAsync($"api/GameApps/GetAllUsersGameRecord");
            return await JsonSerializer.DeserializeAsync<UsersGameRecord>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

   

        public async Task<IEnumerable<UsersGameRecord>> GetUsersGameRecordByGameId(int gameId)
        {
            Stream stream = await _httpClient.GetStreamAsync($"api/GameApps/GetAllUsersGameRecordByGameID/{gameId}");
            return await JsonSerializer.DeserializeAsync<IEnumerable<UsersGameRecord>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

  

        public async Task<UsersGameRecord> GetUsersGameRecordById(int Id)
        {
            Stream stream = await _httpClient.GetStreamAsync($"api/GameApps/GetAllUsersGameRecord/{Id}");
            return await JsonSerializer.DeserializeAsync<UsersGameRecord>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
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
    }
}
