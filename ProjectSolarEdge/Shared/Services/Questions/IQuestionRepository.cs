using ProjectSolarEdge.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSolarEdge.Shared.Services.Questions
{
    public interface IQuestionRepository
    {
        IEnumerable<Question> getQuestions();

        IEnumerable<QuestionAnswer> GetQuestionAnswer();
        Question GetQuestionById(int ID, bool IncludeOptions = true);

        IEnumerable<Question> GetQuestionsThatNotInGameID(int GameID);

        int AddQuestionToDB(Question question);
     //   bool AddQuestionToDB(Question question);


        int AddSubjectToDB(Subject subject);

        //bool AddSubjectToDB(Subject subject);

        bool UpdateQuestion(Question question);
        bool DeleteQuestion(Question question);

        bool DeleteQuestionConnction(int ID);

        //bool DeleteQuestion(int id);

        bool AddAnswerToDB(QuestionAnswer answer);

        bool UpdateAnswer(QuestionAnswer answer);
        bool DeleteAnswer(QuestionAnswer answer);

        //bool DeleteAnswer(int id);

        IEnumerable<Subject> getSubjects();

        bool DeleteSubjectConnction(int ID);
        int AddSubjectToConnection(SubjectsQuestionsConnection subjectsQuestionsConnection);

        //IEnumerable<PlayerGameQuestionsAnswers> GetPlayerGameQuestionsAns(int gameID, int userID);
        bool DeleteSubject(int subjectID);

    }
}
