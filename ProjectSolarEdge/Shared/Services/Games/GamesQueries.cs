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


    }
}




