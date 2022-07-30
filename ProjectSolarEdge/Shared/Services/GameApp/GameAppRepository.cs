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

        public IEnumerable<UsersGameRecord> GetUsersGameRecordByGameId(int ID)
        {
            IEnumerable<UsersGameRecord> _data = GetRecords<UsersGameRecord>(GameAppQueries.GetAllUsersGameRecordByGameID, new { GameID = ID });
            return _data;
        }

        public IEnumerable<UsersGameRecord> GetAllUsersGameRecord()
        {
            IEnumerable<UsersGameRecord> _data = GetRecords<UsersGameRecord>(GameAppQueries.GetAllUsersGameRecordByGameID, null );
            return _data;
        }


        public IEnumerable<UsersGameRecord> AvailableQuestions(int gameID, int userID)
        {
            IEnumerable<UsersGameRecord> _data = GetRecords<UsersGameRecord>(GameAppQueries.AvailableQuestions, new { GameID = gameID, UserID = userID }).ToList();
            return _data;
        }

        public UsersTable GetPlayerByID(int userID)
        {
            UsersTable _user = GetRecords<UsersTable>(GameAppQueries.GetPlayerByID, new { ID = userID }).FirstOrDefault();

            return _user;
        }
    }
}
