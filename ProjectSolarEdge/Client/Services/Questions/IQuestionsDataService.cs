

using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Client.Services.Questions
{
    public interface IQuestionsDataService
    {
        Task<IEnumerable<Question>> GetQuestionsAsync();
        Task<IEnumerable<Subject>> GetSubjectsAsync();

       
        Task<IEnumerable<Question>> GetAllQuestions();

        
        //Task<QuestionAnswer> GetAnswerByIdAsync(int Id);
        Task<Question> GetQuestionByIdAsync(int Id);

        Task<int> AddQuestionToDB(Question newQuestion);

        Task<bool> UpdateQuestion(Question question);
        Task<bool> DeleteQuestion(Question question);

        //Task<bool> DeleteQuestion(int questionId);
 

        Task<bool> AddAnswerToDB(QuestionAnswer answer);

        Task<bool> UpdateAnswer(QuestionAnswer answer);

        Task<bool> DeleteAnswer(QuestionAnswer answer);

        Task<bool> DeleteQuestionAnswers(int questionId);


    }
}
