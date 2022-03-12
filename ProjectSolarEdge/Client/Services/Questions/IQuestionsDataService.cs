using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Client.Services.Questions
{
    public interface IQuestionsDataService
    {
        Task<IEnumerable<Question>> GetQuestionsAsync();
        Task<Question> GetQuestionByIdAsync(int Id);
    }
}
