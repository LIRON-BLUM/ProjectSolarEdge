using ProjectSolarEdge.Shared.Entities;
using System.Text.Json;

namespace ProjectSolarEdge.Client.Services.Games
{
    public class GameDataService : IGamesDataService
    {

        private readonly HttpClient _httpClient;

        public GameDataService(HttpClient client)
        {
            this._httpClient = client;
        }

   

        //public async Task<Game> GetGameByIdAsync(int Id)
        //{
        //    Stream stream = await _httpClient.GetStreamAsync($"api/Questions/Question/{Id}");
        //    return await JsonSerializer.DeserializeAsync<Game>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        //}

        public async Task<IEnumerable<Game>> GetAllGames()
        {
            Stream stream = await _httpClient.GetStreamAsync($"api/Games/GetGames");
            return await JsonSerializer.DeserializeAsync<IEnumerable<Game>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }


        public Task<IEnumerable<Game>> GetGames()
        {
            throw new NotImplementedException();
        }
    }
}
