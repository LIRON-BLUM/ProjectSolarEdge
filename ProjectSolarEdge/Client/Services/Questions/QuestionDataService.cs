using ProjectSolarEdge.Client.Services.Questions;
using ProjectSolarEdge.Shared.Entities;
using System.Text;
using System.Text.Json;

namespace ProjectSolarEdge.Client.Services
{
    public class QuestionDataService : IQuestionsDataService
    {
        private readonly HttpClient _httpClient;

        public QuestionDataService(HttpClient client)
        {
            this._httpClient = client;
        }

        public Task<IEnumerable<Question>> GetAllQuestions()
        {
            throw new NotImplementedException();
        }

        public async Task<Question> GetQuestionByIdAsync(int Id)
        {
            Stream stream = await _httpClient.GetStreamAsync($"api/Questions/Question/{Id}");
            return await JsonSerializer.DeserializeAsync<Question>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }


        public async Task<IEnumerable<Question>> GetQuestionsAsync()
        {
            Stream stream = await _httpClient.GetStreamAsync($"api/Questions/GetQuestions");
            return await JsonSerializer.DeserializeAsync<IEnumerable<Question>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<IEnumerable<Subject>> GetSubjectsAsync()
        {
            Stream stream = await _httpClient.GetStreamAsync($"api/Questions/GetSubjects");
            return await JsonSerializer.DeserializeAsync<IEnumerable<Subject>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }


        public async Task<int> AddQuestionToDB(Question newQuestion)
        {
            var QuestionJson =
                new StringContent(JsonSerializer.Serialize(newQuestion), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Questions/Insert", QuestionJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<int>(await response.Content.ReadAsStreamAsync());
            }

            return 0;
        }



        public async Task<bool> UpdateQuestion(Question question)
        {
            var questionJson =
                new StringContent(JsonSerializer.Serialize(question), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("api/Questions/Question/{questionId}", questionJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<bool>(await response.Content.ReadAsStreamAsync());
            }

            return false;
        }



        public async Task<bool> DeleteQuestion(Question question)
        {
            var questionJson =
                new StringContent(JsonSerializer.Serialize(question), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("api/Questions/DeleteQuestion/{questionId}", questionJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<bool>(await response.Content.ReadAsStreamAsync());
            }

            return false;
        }



        //public async Task<bool> DeleteQuestion(int questionId)
        //{
        //    var response = await _httpClient.DeleteAsync($"api/Questions/Question/{questionId}");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        return await JsonSerializer.DeserializeAsync<bool>(await response.Content.ReadAsStreamAsync());
        //    }

        //    return false;
        //}



        ////////----------Aswers----------//////////
        
        public async Task<bool> AddAnswerToDB(QuestionAnswer answer)
        {
            var AnswerJson =
                new StringContent(JsonSerializer.Serialize(answer), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Questions/InsertAns", AnswerJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<bool>(await response.Content.ReadAsStreamAsync());
            }

            return false;
        }


        

         public async Task<bool> UpdateAnswer(QuestionAnswer answer)
        {
            var AnswerJson =
                new StringContent(JsonSerializer.Serialize(answer), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("api/Questions/Answer/{Id}", AnswerJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<bool>(await response.Content.ReadAsStreamAsync());
            }

            return false;
        }


        public async Task<bool> DeleteAnswer(QuestionAnswer answer)
        {
            var AnswerJson =
                new StringContent(JsonSerializer.Serialize(answer), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("api/Questions/DeleteAnswer/{Id}", AnswerJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<bool>(await response.Content.ReadAsStreamAsync());
            }

            return false;
        }



        public async Task<bool> DeleteQuestionAnswers(int questionId)
        {
            var response = await _httpClient.DeleteAsync($"api/Questions/DeleteAnswer/{questionId}");

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<bool>(await response.Content.ReadAsStreamAsync());
            }

            return false;
        }


        //public async Task<QuestionAnswer> GetAnswerByIdAsync(int Id)
        //{
        //    Stream stream = await _httpClient.GetStreamAsync($"api/QuestionAnswers/QuestionAnswer/{Id}");
        //    return await JsonSerializer.DeserializeAsync<QuestionAnswer>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        //}


    }
}
