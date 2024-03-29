﻿using ProjectSolarEdge.Shared.Data;
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


            IEnumerable<QuestionAnswer> _answers = GetQuestionAnswer();

            foreach (Question question in _data)
            {
                question.Answers = _answers.Where(q => q.QuestionID == question.ID).Select(a => new QuestionAnswer
                {
                    ID = a.ID,
                    AnswerBody = a.AnswerBody
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

        public IEnumerable<QuestionAnswer> GetQuestionAnswer()
        {
            IEnumerable<QuestionAnswer> _data = GetRecords<QuestionAnswer>(QuestionsQueries.GetAllQuestionAnswers, null);
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


        public IEnumerable<Question> GetQuestionsThatNotInGameID(int gameID)
        {
            IEnumerable<Question> _question = GetRecords<Question>(QuestionsQueries.GetQuestionsThatNotInGameID, new { GameID = gameID });

            return _question;
        }

     //   string QuestionBody, DateTime CreationDate, DateTime UpdateDate, QuestionType Type, QuestionDifficulty Difficulty, string Feedback, string Creator
        public int AddQuestionToDB(Question question)
        {

            int results = InsertAndreturnInt(QuestionsQueries.AddNewQuestion, question);


            return results;
        }


        //public bool AddQuestionToDB(Question question)
        //{

        //    bool results = ExecuteAll(QuestionsQueries.UpdateQuestion, question);


        //    return results;
        //}

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

        public bool DeleteQuestionConnction(int ID)
        {
            bool results = ExecuteAll(QuestionsQueries.DeleteQuestionFromConnectionTable, new { QuestionID = ID });


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



        public int AddSubjectToDB(Subject subject)
        {

            int results = InsertAndreturnInt(QuestionsQueries.AddSubject, subject);


            return results;
        }


        //public bool AddSubjectToDB(Subject subject)
        //{

        //    bool results = ExecuteAll(QuestionsQueries.AddSubject, subject);


        //    return results;
        //}

        public int AddSubjectToConnection(SubjectsQuestionsConnection subjectsQuestionsConnection)
        {

            int results = InsertAndreturnInt(QuestionsQueries.InsertSubjectToConnectionTable, subjectsQuestionsConnection);


            return results;
        }

        public bool DeleteSubjectConnction(int ID)
        {
            bool results = ExecuteAll(QuestionsQueries.DeleteSubjectFromConnectionTable, new { QuestionID = ID });


            return results;
        }

        public bool DeleteSubjectByIDFromConnection(int subjectID)
        {
            bool results = ExecuteAll(QuestionsQueries.DeleteSubjectByIDFromConnection, new { SubjectID = subjectID });


            return results;
        }


        public bool DeleteSubject(int subjectID)
        {

            
            bool results = ExecuteAll(QuestionsQueries.DeleteSubject, new { ID = subjectID });


            return results;
        }




        //public IEnumerable<PlayerGameQuestionsAnswers> GetPlayerGameQuestionsAnswers(int gameID, int userID)
        //{
        //    IEnumerable<PlayerGameQuestionsAnswers> _question = GetRecords<Question>(QuestionsQueries.GetPlayerGameQuestionsAnswers, new { GameID = gameID, UserID = userID }).ToList();

        //    foreach (Question question in _question)
        //    {
        //         _question.An = GetRecords<QuestionAnswer>(QuestionsQueries.GetQuestionAnswers, new { ID = _question.ID });
        //    }




        //    return _question;
        //}


    }
}
