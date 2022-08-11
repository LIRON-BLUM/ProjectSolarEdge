using ProjectSolarEdge.Shared.Entities;
using System.Text;
using System.Text.Json;

namespace ProjectSolarEdge.Client.Services.Users
{
    public class UserDataService : IUsersDataService
    {

        private readonly HttpClient _httpClient;

        public UserDataService(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<UsersTable> GetUsererByID(int UserID)
        {
            Stream stream = await _httpClient.GetStreamAsync($"api/UsersTable/GetUserByID/{UserID}");
            return await JsonSerializer.DeserializeAsync<UsersTable>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<UsersTable> GetUserIdByUserName(string UserName)
        {
            Stream stream = await _httpClient.GetStreamAsync($"api/UsersTable/GetUserIdByUserName/{UserName}");
            return await JsonSerializer.DeserializeAsync<UsersTable>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<UsersTable> GetUserIdByUserPassword(string UserPassword)
        {
            Stream stream = await _httpClient.GetStreamAsync($"api/UsersTable/GetUserIdByUserPassword/{UserPassword}");
            return await JsonSerializer.DeserializeAsync<UsersTable>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    

    }
}
