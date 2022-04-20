using Microsoft.AspNetCore.Components;
using ProjectSolarEdge.Client.Services.Questions;
using ProjectSolarEdge.Shared.Entities;
using static System.Net.WebRequestMethods;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
//using MudBlazor.Examples.Data.Models;
using Microsoft.Extensions.DependencyInjection;

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
            //  QuestionsData = await httpClient.GetFromJsonAsync<List<Element>>("webapi/periodictable");

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

        protected async Task NavigateToEdit(int qid)
        {
            if (qid == 4) { 
            NavigationManager.NavigateTo("/EditQuestion/" + qid);
            }
        }


        //public bool dense = false;
        //public bool hover = true;
        //public bool striped = false;
        //public bool bordered = false;
        //public string searchString1 = "";
        //public Question selectedItem1 = null;
        //public HashSet<Question> selectedItems = new HashSet<Question>();

        //public IEnumerable<Question> Elements = new List<Question>();



        //private bool FilterFunc1(Question element) => FilterFunc(element, searchString1);

        //private bool FilterFunc(Question element, string searchString)
        //{
        //    if (string.IsNullOrWhiteSpace(searchString))
        //        return true;
        //    if (element.QuestionBody.Contains(searchString, StringComparison.OrdinalIgnoreCase))
        //        return true;
        //    if (element.Creator.Contains(searchString, StringComparison.OrdinalIgnoreCase))
        //        return true;
        //    if ($"{element.Type} {element.Difficulty} {element.CreationDate}".Contains(searchString))
        //        return true;
        //    return false;
        //}

    }
}
