using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSolarEdge.Shared.Entities
{
    internal class GameQuestionsConnection
    {
        public int ID { get; set; }

        public int QuestionID { get; set; }

        public int GameID { get; set; }

        public int Score { get; set; }

    }
}
