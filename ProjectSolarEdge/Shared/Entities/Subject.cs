using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSolarEdge.Shared.Entities
{
    public class Subject
    {

        public int ID { get; set; }
        public string SubjectName { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime UpdateDate { get; set; }
        public byte isDeleted { get; set; }

    }

    public class SubjectsQuestions
    {
        public int ID { get; set; }

        public string SubjectName { get; set; }

        public int QuestionID { get; set; }
    }
}
