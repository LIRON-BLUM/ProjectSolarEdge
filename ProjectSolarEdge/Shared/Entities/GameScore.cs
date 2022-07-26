using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSolarEdge.Shared.Entities
{
    public class GameScore
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public int GameID { get; set; }

        public int QuestionID { get; set; }

        public int GameElement { get; set; }

        public bool IsRight { get; set; }

        public int GamblingScore { get; set; }

        public int ElementScore { get; set; }

        public bool IsAnswered { get; set; }




    }
}
