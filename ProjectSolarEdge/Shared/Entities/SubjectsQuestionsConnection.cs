﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSolarEdge.Shared.Entities
{
    public class SubjectsQuestionsConnection
    {

        public int ID { get; set; }

        public int QuestionID { get; set; }

        public int SubjectID { get; set; }
    }
}
