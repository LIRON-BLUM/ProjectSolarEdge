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

            IEnumerable<SubjectsQuestions> _subjects = GetQuestionSubjects();

            foreach (Question question in _data)
            {
                question.Subjects = _subjects.Where(q => q.QuestionID == question.ID).Select(s => new Subject
                {
                    ID = s.ID,
                    SubjectName = s.SubjectName
                }).ToList();

            }
            return _data;
        }

        public IEnumerable<Subject> getSubjects()
        {
            IEnumerable<Subject> _data = GetRecords<Subject>(QuestionsQueries.GetAllSubjects, null);
            return _data;
        }

        public IEnumerable<SubjectsQuestions> GetQuestionSubjects()
        {
            IEnumerable<SubjectsQuestions> _data = GetRecords<SubjectsQuestions>(QuestionsQueries.GetQuestionsSubjects, null);
            return _data;
        }


        public Question GetQuestionById(int ID, bool IncludeOptions = true)
        {
            Question _question = GetRecords<Question>(QuestionsQueries.GetQuestionByID, new { ID }).FirstOrDefault();
            _question.Subjects = GetRecords<Subject>(QuestionsQueries.GetSubjectByQuesID, new { QuestionID = ID });


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

        public bool DeleteQuestion(Question question)
        {

            bool results = ExecuteAll(QuestionsQueries.DeleteQuestion, question);


            return results;
        }

        //public bool DeleteQuestion(int ID)
        //{

        //    bool results = ExecuteAll(QuestionsQueries.DeleteQuestion,new {ID = ID });


        //    return results;
        //}

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

        public bool DeleteAnswer(QuestionAnswer answer)
        {

            bool results = ExecuteAll(QuestionsQueries.DeleteAnswer, answer);


            return results;
        }


        //public bool DeleteAnswer(int ID)
        //{

        //    bool results = ExecuteAll(QuestionsQueries.DeleteQuestionAnswer, new { ID = ID });


        //    return results;
        //}




        //public int InsertSubjectConnction(int QuestionID, int SubjetID)
        //{

        //    int results = InsertAndreturnInt(QuestionsQueries.InsertSubjectToConnectionTable, (QuestionID, SubjetID));


        //    return results;
        //}


        public int AddSubjectToConnection(SubjectsQuestionsConnection subjectsQuestionsConnection)
        {

            int results = InsertAndreturnInt(QuestionsQueries.InsertSubjectToConnectionTable, subjectsQuestionsConnection);


            return results;
        }

        //public bool DeleteSubjectConnctionn(SubjectsQuestionsConnection SubjectsQuestionsConnection)
        //{

        //    bool results = ExecuteAll(QuestionsQueries.DeleteSubjectFromConnectionTable, SubjectsQuestionsConnection);


        //    return results;
        //}

        public bool DeleteSubjectConnction(int ID)
        {

            bool results = ExecuteAll(QuestionsQueries.DeleteSubjectFromConnectionTable, new { ID = ID });


            return results;
        }

      

    }
}
