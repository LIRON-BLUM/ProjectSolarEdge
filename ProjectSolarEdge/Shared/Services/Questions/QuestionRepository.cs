using ProjectSolarEdge.Shared.Data;
using ProjectSolarEdge.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSolarEdge.Shared.Services.Questions
{
    public class QuestionRepository : DbRepository, IQuestionRepository
    {
        public QuestionRepository(string connectionString) : base(connectionString)
        {
            
        }

        public IEnumerable<Question> getQuestions()
        {
            IEnumerable<Question> _data = GetRecords<Question>(QuestionsQueries.GetAllQuestions, null);
            return _data;
        }

        public Question GetQuestionById(int ID, bool IncludeOptions = true)
        {
            Question _question = GetRecords<Question>(QuestionsQueries.GetQuestionByID, new { ID }).FirstOrDefault();

            if (IncludeOptions)
            {
                _question.Answers = GetRecords<QuestionAnswers>(QuestionsQueries.GetQuestionOptions, new { ID = _question.ID });
            }

            return _question;
        }
    }
}
