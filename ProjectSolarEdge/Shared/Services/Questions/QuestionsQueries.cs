using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSolarEdge.Shared.Services.Questions
{
    public static class QuestionsQueries
    {
		public static string GetQuestionByID => @"SELECT * FROM Questions WHERE ID = @ID";

		public static string GetQuestionAnswers => @"SELECT * FROM QuestionAnswers WHERE QuestionID = @ID";
		//public static string GetAllQuestions => @"SELECT Q.ID, Q.QuestionBody, Q.Type, Q.Difficulty, Q.CreationDate, Q.UpdateDate, FROM Questions as Q";
		public static string GetAllQuestions => @"SELECT ID, QuestionBody, Type, Difficulty, CreationDate, UpdateDate, Creator, Feedback FROM Questions";


		public static string AddNewQuestion => @"INSERT INTO Question
													(QuestionBody,
													CreationDate,
													UpdateDate,
													Type,
													Difficulty,
													Feedback,
													Creator)
											  VALUES (@QuestionBody, @CreationDate, @UpdateDate, @Type, @Difficulty, @Feedback, @Creator)";

		public static string AddAnswer => @"INSERT INTO QuestionAnswer
													(QuestionID,
													AnswerBody,
													CreationDate,
													UpdateDate,
													IsRight)
											  VALUES (@QuestionID, @AnswerBody, @CreationDate, @UpdateDate, @IsRight)";


	
		public static string UpdateQuestion => @"UPDATE Questions 
												   SET
													QuestionBody = @QuestionBody,
													UpdateDate =  @UpdateDate,
													Type = @Type,
													Difficulty = @Difficulty,
													Feedback =  @Feedback
											    	WHERE  ID = @ID";

		public static string UpdateAnswer => @"UPDATE QuestionAnswer 
												   SET
													AnswerBody = @AnswerBody,
													UpdateDate =  @UpdateDate,
													IsRight = @IsRight
											    	WHERE  ID = @ID";


		public static string DeleteQuestion => @"DELETE FROM Questions WHERE ID = @ID";

	}
}
