using Microsoft.AspNetCore.Components;
using ProjectSolarEdge.Client.Services.Questions;
using ProjectSolarEdge.Shared.Entities;
using static System.Net.WebRequestMethods;

namespace ProjectSolarEdge.Client.Pages
{
    public partial class Questions
    {
        public Question QuestionsCRUD { get; set; } = new Question();
        public QuestionAnswer Answer { get; set; } = new QuestionAnswer();
        public IEnumerable<Question> QuestionsData { get; set; }
        [Inject]
        public IQuestionsDataService QuestionsDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
          QuestionsData = await QuestionsDataService.GetQuestionsAsync();
            

        }

        protected async Task NewQuestion()
        {
           
            NavigationManager.NavigateTo("/EditQuestion");


            // QuestionsCRUD.CreationDate = DateTime.Now;
            //QuestionsCRUD.UpdateDate = DateTime.Now;
            // QuestionsCRUD.Type = (QuestionType)1;
            // QuestionsCRUD.Difficulty = (QuestionDifficulty)1;
            // QuestionsCRUD.Answers = new List<QuestionAnswer>();


            //await QuestionsDataService.AddQuestionToDB(QuestionsCRUD);

            //int QID = QuestionsCRUD.ID;
            ////NavigationManager.NavigateTo("/EditQuestion/{Id}");
            //NavigateToEdit(QID);
           // NavigationManager.NavigateTo("/EditQuestion/"+ QuestionsCRUD.ID);

        }

        //protected void NavigateToEdit(int qid)
        //{
        //    NavigationManager.NavigateTo("/EditQuestion/" + qid);
        //}

    }
}
