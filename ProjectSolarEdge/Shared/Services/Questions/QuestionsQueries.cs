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
		
		public static string GetAllQuestions => @"SELECT ID, QuestionBody, Type, Difficulty, CreationDate, UpdateDate, Creator, Feedback, QuestionImagePath FROM Questions WHERE isDeleted = 0";


		public static string AddNewQuestion => @"INSERT INTO Questions
													(QuestionBody,
													CreationDate,
													UpdateDate,
													Type,
													Difficulty,
													Feedback,
													Creator,
													isDeleted,
													QuestionImagePath)
											  VALUES (@QuestionBody, @CreationDate, @UpdateDate, @Type, @Difficulty, @Feedback, @Creator, 0, @QuestionImagePath)
											  SELECT CAST(SCOPE_IDENTITY() as int)";





		public static string UpdateQuestion => @"UPDATE Questions 
												   SET
													QuestionBody = @QuestionBody,
													UpdateDate =  @UpdateDate,
													Type = @Type,
													Difficulty = @Difficulty,
													Feedback =  @Feedback,
													QuestionImagePath = @QuestionImagePath
											    	WHERE  ID = @ID";



        //public static string DeleteQuestion => @"DELETE FROM Questions WHERE ID = @ID";
        public static string DeleteQuestion => @"UPDATE Questions 
												SET isDeleted = 1
													WHERE ID = @ID";

		public static string DeleteQuestionFromConnectionTable => @"DELETE FROM GameQuestionsConnections WHERE QuestionID = @QuestionID";


		public static string GetQuestionsThatNotInGameID => @"select 
															q.ID,
															q.QuestionBody,
															q.Type,
															q.Difficulty,
															q.Feedback,
															q.Creator,
															q.isDeleted
															from Questions as q
															WHERE q.ID NOT IN (SELECT GQ.QuestionID FROM GameQuestionsConnections as GQ WHERE GQ.GameID = @GameID) and q.isDeleted = 0";


		//-----------------ANSWERS----------------//

		public static string AddAnswer => @"INSERT INTO QuestionAnswers
													(QuestionID,
													AnswerBody,
													CreationDate,
													UpdateDate,
													IsRight,isDeleted, AnswerImagePath)
											  VALUES (@QuestionID, @AnswerBody, @CreationDate, @UpdateDate, @IsRight,0, @AnswerImagePath)";


		public static string UpdateAnswer => @"UPDATE QuestionAnswers 
												   SET
													AnswerBody = @AnswerBody,
													UpdateDate =  @UpdateDate,
													IsRight = @IsRight,
													AnswerImagePath = @AnswerImagePath
											    	WHERE  ID = @ID";


		public static string DeleteAnswer => @"UPDATE QuestionAnswers 
												SET isDeleted = 1
													WHERE ID = @ID";


		public static string DeleteQuestionAnswer => @"DELETE FROM QuestionAnswers WHERE QuestionID = @QuestionID";


		//-----------------Subjects----------------//
		public static string GetSubjectByID => @"SELECT * FROM Subjects WHERE ID = @ID AND isDeleted = 0";

		public static string GetAllSubjects => @"SELECT ID, SubjectName, CreationDate, UpdateDate FROM Subjects WHERE isDeleted = 0";


		public static string GetAllSubjectsName => @"SELECT SubjectName FROM Subjects WHERE isDeleted = 0";


		public static string AddSubject => @"INSERT INTO Subjects (SubjectName, CreationDate, UpdateDate, isDeleted)
											VALUES (@SubjectName, GETDATE(), GETDATE(), 0)";

		public static string InsertSubjectToConnectionTable => @"INSERT INTO SubjectsQuestionsConnection (QuestionID, SubjectID) VALUES (@QuestionID, @SubjectID)  SELECT CAST(SCOPE_IDENTITY() as int)";

		public static string DeleteSubjectFromConnectionTable => @"DELETE FROM SubjectsQuestionsConnection WHERE QuestionID = @QuestionID";





		//Returns Subjects by QuestionID
		public static string GetSubjectByQuesID => @"SELECT 
														S.ID,
														S.SubjectName
													FROM SubjectsQuestionsConnection as SQC
													INNER JOIN Subjects as S ON SQC.SubjectID = S.ID
													WHERE SQC.QuestionID = @QuestionID";



		//Returns all Questions & Subjects
		public static string GetQuestionsSubjects => @"SELECT 
														S.ID,
														S.SubjectName,
														SQC.QuestionID
													FROM SubjectsQuestionsConnection as SQC
													INNER JOIN Subjects as S ON SQC.SubjectID = S.ID";



		public static string GetPlayerGameQuestionsAnswers => @"SELECT 
															q.QuestionBody,
															q.Feedback,
															gs.IsRight as UserIsRight
														FROM Questions as q
														INNER JOIN  GameScore as gs ON q.ID = gs.QuestionID
														WHERE gs.GameID=@GameID AND gs.UserID=@UserID";

	}
}
