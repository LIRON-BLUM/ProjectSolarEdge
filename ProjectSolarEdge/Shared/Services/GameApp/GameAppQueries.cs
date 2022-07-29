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



		public static string GetAllUsersGameRecordByGameID => @"SELECT ID, GameID, UserName, UserFirstName, UserLastName, TotalScore, CreationDate FROM UsersGameRecord
																WHERE GameID = @GameID";


		public static string GetAllUsersGameRecord => @"SELECT ID, GameID, UserName, UserFirstName, UserLastName, TotalScore, CreationDate FROM UsersGameRecord";

		public static string AvailableQuestions => @"select 
														q.ID,
														q.QuestionBody,
														q.Type
													from GameQuestionsConnections as GQ 
													inner join Questions as q ON GQ.QuestionID = q.ID
													WHERE GQ.GameID = @GameID AND GQ.QuestionID NOT IN (SELECT gs.QuestionID FROM GameScore as gs WHERE gs.GameID = @GameID AND gs.UserID = @UserID AND gs.GameElement = 2)";
																
	}
}
