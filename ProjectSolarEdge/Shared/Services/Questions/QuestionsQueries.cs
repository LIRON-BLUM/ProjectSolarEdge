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

		public static string GetQuestionOptions => @"SELECT * FROM QuestionAnswers WHERE QuestionID = @ID";
		public static string GetAllQuestions => @"SELECT 
													Q.ID,
													Q.QuestionBody,
													Q.Type,
													Q.Difficulty,
													Q.CreationDate,
													Q.UpdateDate
												FROM Questions as Q";
	}
}
