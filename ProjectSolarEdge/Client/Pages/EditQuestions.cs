using Microsoft.AspNetCore.Components;
using ProjectSolarEdge.Client.Services.Questions;
using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Client.Pages
{
    public partial class EditQuestions
    {

        [Parameter]
        public string Id { get; set; }

   


        public Question QuestionsCRUD { get; set; } = new Question();

        public List<QuestionAnswer> AnswerList { get; set; } = new List<QuestionAnswer>();

        public QuestionAnswer Answer { get; set; } = new QuestionAnswer();



        public List<QuestionType> QuestionType { get; set; } = new List<QuestionType>();

        [Inject]
        public IQuestionsDataService QuestionDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        protected override async Task OnInitializedAsync()
        {


            int.TryParse(Id, out var QId);

            if (QId == 0) //new Question is being created
            {
                //add some defaults
                QuestionsCRUD = new Question { CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Type = (QuestionType)1, Difficulty = (QuestionDifficulty)1 };
            }
            else
            {
                QuestionsCRUD = await QuestionDataService.GetQuestionByIdAsync(int.Parse(Id));
            }

            

              //if (int.Parse(Id) == 0)
              //{

              //}

              //Answers = await QuestionDataService.GetAnswerByIdAsync(int.Parse(Id));
              QuestionsCRUD = await QuestionDataService.GetQuestionByIdAsync(int.Parse(Id));

        }

        protected async Task AddAndUpdate()
        {
            if (QuestionsCRUD.ID == 0)
            {
              
                QuestionsCRUD.CreationDate = DateTime.Now;
                QuestionsCRUD.UpdateDate = DateTime.Now;
                QuestionsCRUD.Type = (QuestionType)1;
                QuestionsCRUD.Difficulty = (QuestionDifficulty)1;
                QuestionsCRUD.Answers = new List<QuestionAnswer>();

                await QuestionDataService.AddQuestionToDB(QuestionsCRUD);
                await QuestionDataService.AddAnswerToDB(Answer);
                //await QuestionDataService.UpdateAnswer(Answer);
                NavigationManager.NavigateTo("/");

            }
            else
            {
                QuestionsCRUD.Answers = new List<QuestionAnswer>();
                await QuestionDataService.UpdateQuestion(QuestionsCRUD);
                await QuestionDataService.UpdateAnswer(Answer);
                    NavigationManager.NavigateTo("/");
            }

        }

        protected async Task DeleteQuestion()
        {
            await QuestionDataService.DeleteQuestion(QuestionsCRUD.ID);
            NavigationManager.NavigateTo("/");


        }

    }
}
