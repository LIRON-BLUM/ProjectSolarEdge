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

        //Question AddQuestionToDB(string QuestionBody, DateTime CreationDate, DateTime UpdateDate, QuestionType Type, QuestionDifficulty Difficulty, string Feedback, string Creator);


        bool AddQuestionToDB(Question question);

    


        bool UpdateQuestion(Question question);
        bool DeleteQuestion(int id);

        //Question UpdateQuestionByID(int ID);

        //QuestionAnswer GetQuestionAnswerById(int ID, bool IncludeOptions = true);
    }
}
