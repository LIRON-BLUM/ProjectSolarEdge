using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSolarEdge.Shared.Services.GameApp
{
    internal class GameAppQueries
    {


		public static string GetAllGameScore => @"SELECT ID, UserID, GameID, QuestionID, GameElement, IsRight, GamblingScore, ElementScore, IsAnswered FROM GameScore";

		public static string GetAllUserGameScore => @"SELECT ID, UserID, GameID, QuestionID, GameElement, IsRight, GamblingScore, ElementScore, IsAnswered FROM GameScore 
													  WHERE UserID=@UserID AND GameID=@GameID";


		public static string AddNewGameScore => @"INSERT INTO GameScore
													(UserID,
													GameID,
													QuestionID,
													GameElement,
													IsRight,
													GamblingScore,
													ElementScore,
													IsAnswered)
											  VALUES (@UserID, @GameID, @QuestionID, @GameElement, @IsRight, @GamblingScore, @ElementScore, @IsAnswered)
											  SELECT CAST(SCOPE_IDENTITY() as int)";


		public static string UpdateGameScore => @"UPDATE GameScore 
												   SET
													UserID = @UserID,
													GameID =  @GameID,
													QuestionID = @QuestionID,
													GameElement = @GameElement,
													IsRight =  @IsRight,
													GamblingScore = @GamblingScore,
													ElementScore = @ElementScore,
													IsAnswered = @IsAnswered,
											    	WHERE  ID = @ID";


		public static string GetAllUsers => @"SELECT ID, UserFirstName, UserLastName, UserName, UserPassword, UserType, GameStartDate, GameEndDate, isDeleted FROM UsersTable WHERE UserName = @UserName";





        public static string GetGameUsersScore => @"SELECT 
														u.ID,
														u.UserFirstName,
														u.UserLastName,
														SUM(gs.ElementScore) as UserScore
													FROM UsersTable as u
													INNER JOIN  GameScore as gs ON u.ID = gs.UserID
													WHERE gs.GameID = @GameID 
													GROUP BY u.ID, u.UserFirstName, u.UserLastName
													ORDER BY SUM(gs.ElementScore) DESC";

		public static string GetGameUsersScoreByUserID => @"SELECT 
														u.ID,
														u.UserFirstName,
														u.UserLastName,
														SUM(gs.ElementScore) as UserScore
													FROM UsersTable as u
													INNER JOIN  GameScore as gs ON u.ID = gs.UserID
													WHERE gs.GameID = @GameID AND gs.UserID = @UserID
													GROUP BY u.ID, u.UserFirstName, u.UserLastName
													ORDER BY SUM(gs.ElementScore) DESC";

		//public static string UserGameScore => @"SELECT 
		//											SUM(gs.ElementScore) as UserScore
		//											FROM UsersTable as u
		//											INNER JOIN  GameScore as gs ON u.ID = gs.UserID
		//											WHERE gs.GameID = @GameID 
		//											GROUP BY u.ID, u.UserFirstName, u.UserLastName
		//											ORDER BY SUM(gs.ElementScore) DESC";

		//public static string GetUserScoreByID => @"SELECT 
		//											gs.UserID,
		//											SUM(gs.ElementScore) as UserScore
		//										FROM GameScore as gs
		//										WHERE gs.GameID = @GameID AND gs.UserID = @UserID
		//										Group by gs.UserID
		//										ORDER BY SUM(gs.ElementScore) DESC";

		public static string AvailableQuestions => @"select 
														q.ID,
														q.QuestionBody,
														q.Type,
														q.Difficulty,
														q.Feedback
													from GameQuestionsConnections as GQ 
													inner join Questions as q ON GQ.QuestionID = q.ID
													WHERE GQ.GameID = @GameID AND GQ.QuestionID NOT IN (SELECT gs.QuestionID FROM GameScore as gs WHERE gs.GameID = @GameID AND gs.UserID = @UserID AND gs.GameElement = 2)";


		public static string GetPlayerByID => @"select ID, UserFirstName, UserLastName, UserName from UsersTable WHERE ID = @ID";

		public static string AddScoreElement => @"Insert INTO GameScore (UserID,GameID,QuestionID,GameElement,IsRight,GamblingScore,ElementScore,IsAnswered)
													  VALUES (@UserID,@GameID,0,@GameElement,0,@GamblingScore,@ElementScore,0)";


		//public static string UpdateScoreElenent => @"UPDATE GameScore
		//											SET QuestionID = @QuestionID, IsRight = @IsRight, GamblingScore = @GamblingScore, ElementScore= @ElementScore, IsAnswered = @IsAnswered
		//											WHERE ID = (SELECT MAX(ID) FROM GameScore WHERE UserID = @UserID AND GameID = @GameID)";


		public static string UpdateScoreElement => @"UPDATE GameScore
													SET QuestionID = @QuestionID, IsRight = @IsRight, GamblingScore = @GamblingScore, ElementScore= @ElementScore, IsAnswered = @IsAnswered
													WHERE UserID = @UserID AND GameID = @GameID AND QuestionID = 0 AND GameElement = 2";


		public static string GetQuestionScoreByGameID => @"SELECT * FROM GameQuestionsConnections
															WHERE QuestionID = @QuestionID AND GameID = @GameID";


		public static string GetGamblingScore => @"SELECT * FROM GameScore
															WHERE UserID = @UserID AND GameID = @GameID AND QuestionID = 0 AND GameElement = 2";


		//Select all the Questions in a specific game with a specific player
		//public static string GetPlayerGameQuestions => @"SELECT * FROM GameScore WHERE GameID=@GameID AND UserID=@UserID";
	}
}
