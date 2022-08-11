using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSolarEdge.Shared.Entities
{
    public class QuestionAnswer
    {
        public int ID { get; set; }

        public int QuestionID { get; set; }
        public string AnswerBody { get; set; }
        public DateTime CreationDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public bool IsRight { get; set; }

        public byte isDeleted { get; set; }

        public int QuestionOrder { get; set;}

      





    }
}
