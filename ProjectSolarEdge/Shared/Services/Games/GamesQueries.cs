using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSolarEdge.Shared.Data;
using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Shared.Services.Games
{
    internal class GamesQueries
    {

        public static string GetGameByID => @"SELECT * FROM Games WHERE ID = @ID AND isDeleted = 0";

        //public static string GetAllGames => @"SELECT * FROM Games WHERE isDeleted = 0";

        public static string GetAllGames => @"SELECT ID,  GameName, GameDescription, CreationDate, UpdateDate, IsPublished, GameTheme,GameStartDate,GameEndDate,CreatorID,GameTimeLimit, ScoreMethod,ScoreEasy,ScoreMedium, ScoreHard,IsGamified,WheelIteration,GambleIteration FROM Games WHERE isDeleted = 0";


        public static string AddGame => @"INSERT INTO Games (GameName, GameDescription, CreationDate, UpdateDate, IsPublished, GameTheme,GameStartDate,GameEndDate,CreatorID,GameTimeLimit, ScoreMethod,ScoreEasy,ScoreMedium, ScoreHard,IsGamified,WheelIteration,GambleIteration, isDeleted)
                                        VALUES (@GameName,@GameDescription, @CreationDate, @UpdateDate, 0, @GameTheme, @GameStartDate, @GameStartDate, @CreatorID,@GameTimeLimit,@ScoreMethod,@ScoreEasy, @ScoreMedium, @ScoreHard, @IsGamified, @WheelIteration, @GambleIteration, 0)";


        //public static string AddGame => @"INSERT INTO Games (GameName, GameDescription, CreationDate, UpdateDate, IsPublished, GameTheme,GameStartDate,GameEndDate,CreatorID,GameTimeLimit, ScoreMethod,ScoreEasy,ScoreMedium, ScoreHard,IsGamified,WheelIteration,GambleIteration, isDeleted)
        //                                    VALUES ('Game Name','This is the game Description',GETDATE(), GETDATE(), 0, 1, GETDATE(),GETDATE(),1,10,1,200, 300, 400, 1, 2, 3, 0)";

        public static string UpdateGame => @"UPDATE Games 
        SET
        GameName=@GameName,
        GameDescription=@GameDescription,
        UpdateDate=@UpdateDate,
        IsPublished=@IsPublished,
        GameTheme=@GameTheme,
        GameStartDate=@GameStartDate,
        GameEndDate=@GameEndDate,
        GameTimeLimit=@GameTimeLimit,
        ScoreMethod =@ScoreMethod,
        ScoreEasy=@ScoreEasy,
        ScoreMedium=@ScoreMedium,
        ScoreHard=@ScoreHard,
        IsGamified=@IsGamified,
        WheelIteration=@WheelIteration,
        GambleIteration=@GambleIteration
        WHERE ID=@ID";

        public static string InsertQuestionToConnectionTable => @"INSERT INTO GameQuestionsConnections (QuestionID, GameID, Score) VALUES (@QuestionID, @GameID, 0)  SELECT CAST(SCOPE_IDENTITY() as int)";

        public static string DeleteQuestionFromConnectionTable => @"DELETE FROM GameQuestionsConnections WHERE GameID = @GameID";




        //Returns Questions by QuestionID
        public static string GetQuestionByGameID => @"SELECT 
                                                    Q.ID,
                                                    Q.QuestionBody,
                                                    Q.Type,
                                                    Q.UpdateDate,
                                                    Q.Difficulty
                                                    FROM GameQuestionsConnections as GQC
                                                    INNER JOIN Questions as Q ON GQC.QuestionID = Q.ID
                                                    WHERE GQC.GameID = @GameID";



        //Returns all Gaames & Questions
        public static string GetGameQuestions => @"SELECT 
                                                        Q.ID,
                                                        Q.QuestionBody,
                                                        GQC.GameID
                                                        FROM GameQuestionsConnections as GQC
                                                        INNER JOIN Questions as Q ON GQC.QuestionID = Q.ID";
    }
}




