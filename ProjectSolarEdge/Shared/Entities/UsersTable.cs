using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSolarEdge.Shared.Entities
{
    internal class UsersTable
    {
        public int ID { get; set; }
        
        public string UserFirstName { get; set; }
        
        public string UserLastName { get; set; }
        
        public string UserName { get; set; }
        
        public string UserPassword { get; set; }
        
        public UserType UserType { get; set; }

        public DateTime GameStartDate { get; set; }

        public DateTime GameEndDate { get; set; }
        public byte isDeleted { get; set; }
    }

    public enum UserType
    {
        Learnre = 1,
        QuestionEditor = 2,
        GameEditor = 3
    }
}
