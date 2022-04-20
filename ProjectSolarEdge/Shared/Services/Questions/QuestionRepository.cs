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
                _question.Answers = GetRecords<QuestionAnswer>(QuestionsQueries.GetQuestionAnswers, new { ID = _question.ID });
            }

            return _question;
        }

        //string QuestionBody, DateTime CreationDate, DateTime UpdateDate, QuestionType Type, QuestionDifficulty Difficulty, string Feedback, string Creator
        public int AddQuestionToDB(Question question)
        {

           int results = InsertAndreturnInt(QuestionsQueries.AddNewQuestion, question);
        

            return results;
        }

        public bool UpdateQuestion(Question question)
        {

            bool results = ExecuteAll(QuestionsQueries.UpdateQuestion, question);


            return results;
        }

        public bool DeleteQuestion(int ID)
        {

            bool results = ExecuteAll(QuestionsQueries.DeleteQuestion,new {ID = ID });


            return results;
        }

        public bool AddAnswerToDB(QuestionAnswer answer)
        {

            bool results = ExecuteAll(QuestionsQueries.AddAnswer, answer);


            return results;
        }

        

        public bool UpdateAnswer(QuestionAnswer answer)
        {

            bool results = ExecuteAll(QuestionsQueries.UpdateAnswer, answer);


            return results;
        }

        public bool DeleteAnswer(int ID)
        {

            bool results = ExecuteAll(QuestionsQueries.DeleteQuestionAnswer, new { ID = ID });


            return results;
        }

 
    }
}
