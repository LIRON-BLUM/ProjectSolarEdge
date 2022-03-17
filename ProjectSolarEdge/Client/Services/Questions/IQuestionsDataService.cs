

using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Client.Services.Questions
{
    public interface IQuestionsDataService
    {
        Task<IEnumerable<Question>> GetQuestionsAsync();
        Task<IEnumerable<Question>> GetAllQuestions();

        
        //Task<QuestionAnswer> GetAnswerByIdAsync(int Id);
        Task<Question> GetQuestionByIdAsync(int Id);

        Task<bool> AddQuestionToDB(Question newQuestion);

        Task<bool> UpdateQuestion(Question question);

        Task<bool> DeleteQuestion(int questionId);


        }
}
