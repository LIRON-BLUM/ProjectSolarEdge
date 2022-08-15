using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSolarEdge.Shared.Entities
{
    internal class PlayerGameQuestionsAnswers
    {
        public int QuestionID { get; set; }



        public string QuestionBody { get; set; }

        public string Feedback { get; set; }

        public byte UserIsRight { get; set; }

        public List<QuestionAnswer> Answers { get; set; }

    }
}
