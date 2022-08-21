using ProjectSolarEdge.Shared.Entities;
using System.Text;
using System.Text.Json;

namespace ProjectSolarEdge.Client.Services.Questions
{
    public class QuestionDataService : IQuestionsDataService
    {
        private readonly HttpClient _httpClient;

        public QuestionDataService(HttpClient client)
        {
            _httpClient = client;
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

        public async Task<IEnumerable<Question>> GetQuestionsThatNotInGameID(int GameID)
        {
            Stream stream = await _httpClient.GetStreamAsync($"api/Questions/GetQuestionsThatNotInGameID/{GameID}");
            return await JsonSerializer.DeserializeAsync<IEnumerable<Question>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
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


        //public async Task<bool> AddQuestionToDB(Question newQuestion)
        //{
        //    var QuestionJson =
        //        new StringContent(JsonSerializer.Serialize(newQuestion), Encoding.UTF8, "application/json");

        //    var response = await _httpClient.PostAsync($"api/Questions/Insert", QuestionJson);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        return await JsonSerializer.DeserializeAsync<bool>(await response.Content.ReadAsStreamAsync());
        //    }

        //    return false;
        //}



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

            var response = await _httpClient.PutAsync("api/Questions/DeleteQuestion/{question}", questionJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<bool>(await response.Content.ReadAsStreamAsync());
            }

            return false;
        }


        public async Task<bool> DeleteQuestionConnection(int Id)
        {
            var response = await _httpClient.DeleteAsync($"api/Questions/DeleteQuestionConnction/{Id}");

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<bool>(await response.Content.ReadAsStreamAsync());
            }

            return false;
        }


        ////////----------Subject----------//////////
        public async Task<IEnumerable<Subject>> GetSubjectsAsync()
        {
            Stream stream = await _httpClient.GetStreamAsync($"api/Questions/GetSubjects");
            return await JsonSerializer.DeserializeAsync<IEnumerable<Subject>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }




        public async Task<bool> DeleteSubjectConnection(int Id)
        {
            var response = await _httpClient.DeleteAsync($"api/Questions/SubjectConnection/{Id}");

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<bool>(await response.Content.ReadAsStreamAsync());
            }

            return false;
        }



        public async Task<int> AddSubjectConnection(SubjectsQuestionsConnection subjectsQuestionsConnection)
        {
            var AnswerJson =
                new StringContent(JsonSerializer.Serialize(subjectsQuestionsConnection), Encoding.UTF8, "application/json");

            new StringContent(JsonSerializer.Serialize(subjectsQuestionsConnection), Encoding.UTF8, "application/json");


            new StringContent(JsonSerializer.Serialize(subjectsQuestionsConnection), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"api/Questions/InsertSubConnection", AnswerJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<int>(await response.Content.ReadAsStreamAsync());
            }

            return 0;
        }

        public async Task<int> AddSubjectToDB(Subject subject)
        {
            var SubjectJson =
                new StringContent(JsonSerializer.Serialize(subject), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Questions/InsertSubject", SubjectJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<int>(await response.Content.ReadAsStreamAsync());
            }

            return 0;
        }




        ////////----------Aswers----------//////////

        public async Task<bool> AddAnswerToDB(QuestionAnswer answer)
        {
            var AnswerJson =
                new StringContent(JsonSerializer.Serialize(answer), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"api/Questions/InsertAns", AnswerJson);

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

   



        //public Task InsertSubjectConnction(List<Subject> selectedSubjectToUpdate)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task InsertSubjectConnction(int QuestionID, int SubjetID)
        //{
        //    throw new NotImplementedException();
        //}


        //public async Task<QuestionAnswer> GetAnswerByIdAsync(int Id)
        //{
        //    Stream stream = await _httpClient.GetStreamAsync($"api/QuestionAnswers/QuestionAnswer/{Id}");
        //    return await JsonSerializer.DeserializeAsync<QuestionAnswer>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        //}


    }
}
