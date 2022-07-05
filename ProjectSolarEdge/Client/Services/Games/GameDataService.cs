using ProjectSolarEdge.Shared.Entities;
using System.Text;
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

        public async Task<Game> GetGameByIdAsync(int Id)
        {
            Stream stream = await _httpClient.GetStreamAsync($"api/Games/Game/{Id}");
            return await JsonSerializer.DeserializeAsync<Game>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<bool> UpdateGame(Game game)
        {
            try
            {
                var gameJson =
                new StringContent(JsonSerializer.Serialize(game), Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"api/Games/UpdateGame/{game.ID}", gameJson);

                if (response.IsSuccessStatusCode)
                {
                    return await JsonSerializer.DeserializeAsync<bool>(await response.Content.ReadAsStreamAsync());
                }

                return false;
            }
            catch (Exception ex)
            {

                return false;
            }
            
        }

        public async Task<int> AddGameToDB(Game game)
        {
            var GameJson =
                           new StringContent(JsonSerializer.Serialize(game), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Questions/InsertSubject", GameJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<int>(await response.Content.ReadAsStreamAsync());
            }

            return 0;
        }
    }
}
