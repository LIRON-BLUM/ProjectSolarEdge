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
        

        Question GetQuestionById(int ID, bool IncludeOptions = true);

        int AddQuestionToDB(Question question);


        bool UpdateQuestion(Question question);
        bool DeleteQuestion(Question question);
        
        //bool DeleteQuestion(int id);

        bool AddAnswerToDB(QuestionAnswer answer);

        bool UpdateAnswer(QuestionAnswer answer);
        bool DeleteAnswer(QuestionAnswer answer);

        //bool DeleteAnswer(int id);

        IEnumerable<Subject> getSubjects();

        bool DeleteSubjectConnction(int ID);
        int AddSubjectToConnection(SubjectsQuestionsConnection subjectsQuestionsConnection);


    }
}
