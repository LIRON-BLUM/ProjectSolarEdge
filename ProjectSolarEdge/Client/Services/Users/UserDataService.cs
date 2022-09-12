using ProjectSolarEdge.Client.Pages;
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

      

        public async Task<IEnumerable<UsersTable>> GetAllUsers()
        {
            Stream stream = await _httpClient.GetStreamAsync($"api/UsersTable/GetAllUsers");
            return await JsonSerializer.DeserializeAsync<IEnumerable<UsersTable>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
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

        public async Task<int> AddUserToDB(UsersTable newUser)
        {
            var UserJson =
             new StringContent(JsonSerializer.Serialize(newUser), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/UsersTable/AddUser", UserJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<int>(await response.Content.ReadAsStreamAsync());
            }

            return 0;
        }

        public async Task<bool> DeleteUser(UsersTable user)
        {
            var UserJson =
                 new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/UsersTable/DeleteUser/{user}", UserJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<bool>(await response.Content.ReadAsStreamAsync());
            }

            return false;
        }
        public async Task<bool> UpdateUser(UsersTable user)
        {
            var UserJson =
               new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/UsersTable/UpdateUser/{user}", UserJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<bool>(await response.Content.ReadAsStreamAsync());
            }

            return false;
        }
    








    }
}
