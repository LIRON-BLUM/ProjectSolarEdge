using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSolarEdge.Shared.Services.Questions
{
    public static class QuestionsQueries
    {

		//-----------------Questions----------------//
		public static string GetQuestionByID => @"SELECT * FROM Questions WHERE ID = @ID AND isDeleted = 0";

		public static string GetQuestionAnswers => @"SELECT * FROM QuestionAnswers WHERE QuestionID = @ID AND isDeleted = 0";
		
		public static string GetAllQuestions => @"SELECT ID, QuestionBody, Type, Difficulty, CreationDate, UpdateDate, Creator, Feedback FROM Questions WHERE isDeleted = 0";


		public static string AddNewQuestion => @"INSERT INTO Questions
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


		public static string UpdateQuestion => @"UPDATE Questions 
												   SET
													QuestionBody = @QuestionBody,
													UpdateDate =  @UpdateDate,
													Type = @Type,
													Difficulty = @Difficulty,
													Feedback =  @Feedback
											    	WHERE  ID = @ID";



        //public static string DeleteQuestion => @"DELETE FROM Questions WHERE ID = @ID";
        public static string DeleteQuestion => @"UPDATE Questions 
												SET isDeleted = 1
													WHERE ID = @ID";


		//-----------------ANSWERS----------------//

		public static string AddAnswer => @"INSERT INTO QuestionAnswers
													(QuestionID,
													AnswerBody,
													CreationDate,
													UpdateDate,
													IsRight)
											  VALUES (@QuestionID, @AnswerBody, @CreationDate, @UpdateDate, @IsRight)";


		public static string UpdateAnswer => @"UPDATE QuestionAnswers 
												   SET
													AnswerBody = @AnswerBody,
													UpdateDate =  @UpdateDate,
													IsRight = @IsRight
											    	WHERE  ID = @ID";


		public static string DeleteAnswer => @"UPDATE QuestionAnswers 
												SET isDeleted = 1
													WHERE ID = @ID";


		public static string DeleteQuestionAnswer => @"DELETE FROM QuestionAnswers WHERE QuestionID = @QuestionID";


		//-----------------Subjects----------------//
		public static string GetSubjectByID => @"SELECT * FROM Subjects WHERE ID = @ID AND isDeleted = 0";

		public static string GetAllSubjects => @"SELECT * FROM Subjects WHERE isDeleted = 0";


		public static string GetAllSubjectsName => @"SELECT SubjectName FROM Subjects WHERE isDeleted = 0";

		public static string GetSubjectByQuesID => @"SELECT 
														S.ID,
														S.SubjectName
													FROM SubjectsQuestionsConnection as SQC
													INNER JOIN Subjects as S ON SQC.SubjectID = S.ID
													WHERE SQC.QuestionID = @QuestionID";
	}
}
