using ProjectSolarEdge.Shared.Entities;
using System.Text.Json;

namespace ProjectSolarEdge.Client.Services.Questions
{
    public class QuestionDataService : IQuestionsDataService
    {
        private readonly HttpClient _httpClient;

        public QuestionDataService(HttpClient client)
        {
            this._httpClient = client;
        }
        public async Task<Question> GetQuestionByIdAsync(int Id)
        {

            return await JsonSerializer.DeserializeAsync<Question>(await _httpClient.GetStreamAsync($"api/Questions/Question/{Id}"));
        }

        public async Task<IEnumerable<Question>> GetQuestionsAsync()
        {
            Stream stream = await _httpClient.GetStreamAsync($"api/Questions/GetQuestions");
            return await JsonSerializer.DeserializeAsync<IEnumerable<Question>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
