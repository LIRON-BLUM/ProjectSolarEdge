using ProjectSolarEdge.Shared.Data;
using ProjectSolarEdge.Shared.Entities;
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

        public IEnumerable<UsersGameRecord> GetAllUsersGameRecordByGameID(int ID)
        {
            IEnumerable<UsersGameRecord> _data = GetRecords<UsersGameRecord>(GameAppQueries.GetAllUsersGameRecordByGameID, new { GameID = ID }).ToList();
            return _data;
        }

        public IEnumerable<UsersGameRecord> GetAllUsersGameRecord()
        {
            IEnumerable<UsersGameRecord> _data = GetRecords<UsersGameRecord>(GameAppQueries.GetAllUsersGameRecordByGameID, null );
            return _data;
        }


    }
}
