using ProjectSolarEdge.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSolarEdge.Shared.Services.GameApp
{
    public class GameAppRepository : DbRepository, IGameAppRepository
    {
        public GameAppRepository(string connectionString) : base(connectionString)
        {




	}
    }
}
