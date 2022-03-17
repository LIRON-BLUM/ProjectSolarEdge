using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSolarEdge.Shared.Entities
{
    public class Question
    {

        public int ID { get; set; }
        public string QuestionBody { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public QuestionType Type { get; set; }

        public QuestionDifficulty Difficulty { get; set; }

        public string Feedback { get; set; }

        public string Creator { get; set; }

        public IEnumerable<QuestionAnswer> Answers { get; set; }

    }

    public enum QuestionType
    {
        SingleChoice = 1,
        MultipleChoice = 2,
        TrueFalse = 3
    }

    public enum QuestionDifficulty
    {
        Easy = 1,
        Medium = 2,
        Hard = 3
    }
}
