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
    }
}
