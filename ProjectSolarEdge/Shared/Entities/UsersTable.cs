using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSolarEdge.Shared.Entities
{
    public class UsersTable
    {
        public int ID { get; set; }
        
        public string UserFirstName { get; set; }
        
        public string UserLastName { get; set; }
        
        public string UserName { get; set; }
        
        public string UserPassword { get; set; }
        
        public UserType UserType { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime UpdateDate { get; set; }
        public byte isDeleted { get; set; }

      
    }

    public enum UserType
    {
        Learner = 1,
        Editor = 2,
        Admin = 3
    }


}
