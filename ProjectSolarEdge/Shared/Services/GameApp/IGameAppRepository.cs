using ProjectSolarEdge.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSolarEdge.Shared.Services.GameApp
{
    public interface IGameAppRepository
    {
        //IEnumerable<UsersGameRecord> GetUsersGameRecordByGameId(int ID);

        //IEnumerable<UsersGameRecord> GetAllUsersGameRecord();

        IEnumerable<Question> AvailableQuestions(int gameID, int userID);

        UsersTable GetPlayerByID(int userID);

        int AddScoreElement(GameScore GameScore);

        bool UpdateScoreElement(GameScore GameScore);

        IEnumerable<GameScore> GetAllUserGameScore(int gameID, int userID);


        IEnumerable<UserGameScore> GetUsersScore(int gameID);

        UserGameScore GetGameUserScoreByUserID(int gameID, int userID);

        GameQuestionsConnection GetQuestionScoreByGameID (int gameID, int questionID);

        GameScore GetGamblingScore(int gameID, int userID);

        //IEnumerable<GameScore> GetPlayerGameQuestions(int gameID, int userID);



    }
}
