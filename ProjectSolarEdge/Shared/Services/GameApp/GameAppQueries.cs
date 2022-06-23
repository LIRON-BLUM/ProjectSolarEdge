using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSolarEdge.Shared.Services.GameApp
{
    internal class GameAppQueries
    {


		public static string GetAllGameScore => @"SELECT ID, QuestionBody, Type, Difficulty, CreationDate, UpdateDate, Creator, Feedback FROM GameScore WHERE isDeleted = 0";


		public static string AddNewGameScore => @"INSERT INTO GameScore
													(QuestionBody,
													CreationDate,
													UpdateDate,
													Type,
													Difficulty,
													Feedback,
													Creator,
													isDeleted,
													SubjectID)
											  VALUES (@QuestionBody, @CreationDate, @UpdateDate, @Type, @Difficulty, @Feedback, @Creator, 0, @SubjectID)
											  SELECT CAST(SCOPE_IDENTITY() as int)";


		public static string UpdateGameScore => @"UPDATE GameScore 
												   SET
													QuestionBody = @QuestionBody,
													UpdateDate =  @UpdateDate,
													Type = @Type,
													Difficulty = @Difficulty,
													Feedback =  @Feedback
											    	WHERE  ID = @ID";

	}
}
