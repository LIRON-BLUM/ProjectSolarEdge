using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSolarEdge.Shared.Entities
{
    public class Game
    {
        public int ID { get; set; }

        public string GameName { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public string GameDescription { get; set; }

        public byte isPublished { get; set; }

        public GameTheme GameTheme { get; set; }

        public DateTime GameStartDate { get; set; }

        public DateTime GameEndDate { get; set; }

        public string Feedback { get; set; }

        public int CreatorID { get; set; }

        //public List<QuestionAnswer> Answers { get; set; }

        public int GameTimeLimit { get; set; }

        public ScoreMethod ScoreMethod { get; set; }
     
        public int ScoreEasy { get; set; }

        public int ScoreMedium { get; set; }

        public int ScoreHard { get; set; }

        public byte IsGamified { get; set; }

        public int WheelIteration { get; set; }

        public int GambleIteration { get; set; }

        public byte isDeleted { get; set; }

        public static implicit operator Game(Question v)
        {
            throw new NotImplementedException();
        }
    }

    public enum GameTheme
    {
        ThemeA = 1,
        ThemeB = 2,
        ThemeC = 3,
        ThemeD = 4
    }

    public enum ScoreMethod
    {
        ScoreMethodA = 1,
        ScoreMethodB = 2,
        ScoreMethodC = 3
    }
}
