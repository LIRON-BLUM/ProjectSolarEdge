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

        //public IEnumerable<UsersGameRecord> GetUsersGameRecordByGameId(int ID)
        //{
        //    IEnumerable<UsersGameRecord> _data = GetRecords<UsersGameRecord>(GameAppQueries.GetAllUsersGameRecordByGameID, new { GameID = ID });
        //    return _data;
        //}

        //public IEnumerable<UsersGameRecord> GetAllUsersGameRecord()
        //{
        //    IEnumerable<UsersGameRecord> _data = GetRecords<UsersGameRecord>(GameAppQueries.GetAllUsersGameRecordByGameID, null );
        //    return _data;
        //}





        public IEnumerable<Question> AvailableQuestions(int gameID, int userID)
        {
            IEnumerable<Question> _data = GetRecords<Question>(GameAppQueries.AvailableQuestions, new { GameID = gameID, UserID = userID }).ToList();
            return _data;
        }

        public UsersTable GetPlayerByID(int userID)
        {
            UsersTable _user = GetRecords<UsersTable>(GameAppQueries.GetPlayerByID, new { ID = userID }).FirstOrDefault();

            return _user;
        }

        public int AddScoreElement(GameScore GameScore)
        {
            int results = InsertAndreturnInt(GameAppQueries.AddScoreElement, GameScore);


            return results;
        }

        public bool UpdateScoreElement(GameScore GameScore)
        {

            bool results = ExecuteAll(GameAppQueries.UpdateScoreElement, GameScore);


            return results;
        }

        public IEnumerable<GameScore> GetAllUserGameScore(int gameID, int userID)
        {
            IEnumerable<GameScore> _data = GetRecords<GameScore>(GameAppQueries.AvailableQuestions, new { GameID = gameID, UserID = userID }).ToList();
            return _data;
        }

   


        public IEnumerable<UserGameScore> GetUsersScore(int gameID)
        {
            IEnumerable<UserGameScore> _data = GetRecords<UserGameScore>(GameAppQueries.GetGameUsersScore, new { GameID = gameID });
            return _data;
        }

        public UserGameScore GetGameUserScoreByUserID(int gameID, int userID)
        {
            UserGameScore _data = GetRecords<UserGameScore>(GameAppQueries.GetGameUsersScoreByUserID, new { GameID = gameID, UserID = userID }).FirstOrDefault();
            return _data;
        }
    }
}
