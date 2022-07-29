using ProjectSolarEdge.Shared.Entities;
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

        public async Task<UsersGameRecord> GetUsersGameRecord()
        {
            Stream stream = await _httpClient.GetStreamAsync($"api/GameApps/GetAllUsersGameRecord");
            return await JsonSerializer.DeserializeAsync<UsersGameRecord>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<UsersGameRecord> GetUsersGameRecordById(int Id)
        {
            Stream stream = await _httpClient.GetStreamAsync($"api/GameApps/GetAllUsersGameRecord/{Id}");
            return await JsonSerializer.DeserializeAsync<UsersGameRecord>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
