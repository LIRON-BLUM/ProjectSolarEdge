using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSolarEdge.Shared.Entities
{
    internal class Creator
    {
        public int ID { get; set; }

        public string CreatorName { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public UserRole UserRole { get; set; }

        public string CreatorEmail { get; set; }

        public string CreatorPassword { get; set; }

    }

    public enum UserRole
    {
        ContentCreator = 1,
        GamesCreator = 2,
        Admin = 3
    }
}
