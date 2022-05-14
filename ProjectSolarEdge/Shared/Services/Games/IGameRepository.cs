using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Shared.Services.Games
{
    internal interface IGameRepository
    {

        public static string GetQuestionByID => @"SELECT * FROM Games WHERE ID = @ID AND isDeleted = 0";

    }
}
