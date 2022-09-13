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

        public string? Feedback { get; set; }

        public string? Creator { get; set; }

        public List<QuestionAnswer>? Answers { get; set; }
        public List<Subject>? Subjects { get; set; }
        public byte isDeleted { get; set; }

        public string? QuestionImagePath { get; set; }


        public static implicit operator Question(QuestionAnswer v)
        {
            throw new NotImplementedException();
        }



    }

    public enum QuestionType
    {
        //SingleChoice = 1,
        //MultipleChoice = 2,
        //TrueFalse = 3

        MultipleChoice = 1,
        TrueFalse = 2,
        Order = 3
    }

    public enum QuestionDifficulty
    {
        Easy = 1,
        Medium = 2,
        Hard = 3
    }

    public class GamesQuestions
    {
        public int ID { get; set; }

        public string QuestionBody { get; set; }

        public QuestionType Type { get; set; }

        public QuestionDifficulty Difficulty { get; set; }

        public DateTime UpdateDate { get; set; }

        public int GameID { get; set; }

  
    }
}
