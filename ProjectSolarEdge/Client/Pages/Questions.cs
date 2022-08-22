using Microsoft.AspNetCore.Components;
using ProjectSolarEdge.Client.Services.Questions;
using ProjectSolarEdge.Shared.Entities;
using static System.Net.WebRequestMethods;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
//using MudBlazor.Examples.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using ProjectSolarEdge.Client.Services;
using ProjectSolarEdge.Client.Shared;

namespace ProjectSolarEdge.Client.Pages
{
  
    public partial class Questions : ComponentBase, IDisposable
    {

        [Parameter]
        public string EditorID { get; set; }

        public int Eid { get; set; }

        public Question AllQuestions { get; set; } = new Question();

        public Question QuestionsToDelete { get; set; } = new Question();

        public Question QuestionsToEdit { get; set; } = new Question();

        public QuestionAnswer Answer { get; set; } = new QuestionAnswer();

        public IEnumerable<Question> QuestionsData { get; set; }
        public IEnumerable<Question> QuestionsDataToDisplay { get; set; }

        public IEnumerable<SubjectsQuestions> SubjectsData { get; set; }

        [CascadingParameter]
        public MainLayout MainLayout { get; set; }

        [CascadingParameter]
        public NavMenu NavLayout { get; set; }

        [Inject]
        public IQuestionsDataService QuestionDataService { get; set; }


        [Inject]
        public NavigationManager NavigationManager { get; set; }


        [Inject]
        public Blazored.LocalStorage.ISyncLocalStorageService LocalService { get; set; }

        string EditorIDSessiom = "";


        protected override async Task OnInitializedAsync()
        {
          QuestionsData = await QuestionDataService.GetQuestionsAsync();
            QuestionsDataToDisplay = QuestionsData;
            //SubjectsData = await QuestionDataService.GetSubjectsAsync();
            //  QuestionsData = await httpClient.GetFromJsonAsync<List<Element>>("webapi/periodictable");

            EditorIDSessiom = LocalService.GetItem<string>("SessionValue");

            string test = EditorIDSessiom;

        }


        

    protected async Task NavMultipleQuestion()
        {
            NavigationManager.NavigateTo("./EditQuestion");
        }

    protected async Task NavYesNoQuestion()
        {
            NavigationManager.NavigateTo("./Questions");
        }

    protected async Task NavOrderQuestion()
        {
            NavigationManager.NavigateTo("./OrderQuestionEdit");
        }

        protected async Task NavigateToEdit(int qid)
        {

            QuestionsToEdit = await QuestionDataService.GetQuestionByIdAsync(qid);

            if (QuestionsToEdit.Type == QuestionType.MultipleChoice) 
            { 
                NavigationManager.NavigateTo($"./EditQuestion/{qid}");
            }
            else if (QuestionsToEdit.Type == QuestionType.Order)
            {
                NavigationManager.NavigateTo($"./OrderQuestionEdit/{qid}"); 
            }
            else
            {
                NavigationManager.NavigateTo($"./EditQuestion/{qid}");
            }
        }


        protected async Task NavNewQuestion()
        {
            NavigationManager.NavigateTo($"./EditQuestion");
        }


        bool fixed_header = true;
        bool fixed_footer = false;

        //private string searchString = "";
        private string searchString = "";
        private int totalItems;
        private IEnumerable<Question> pagedData;
        private MudTable<Question> table;
        public string SubName= "";

        //private bool FilterFunc(Question question)

        protected async Task<TableData<Question>> ServerReload(TableState state)
        {

            IEnumerable<Question> data = await QuestionDataService.GetQuestionsAsync();
          
        
            switch (state.SortLabel)
            {
                case "ID_field":
                    data = data.OrderByDirection(state.SortDirection, o => o.ID);
                    break;
                case "Type_field":
                    data = data.OrderByDirection(state.SortDirection, o => o.Type);
                    break;
                case "Difficulty_field":
                    data = data.OrderByDirection(state.SortDirection, o => o.Difficulty);
                    break;
                case "Creator_field":
                    data = data.OrderByDirection(state.SortDirection, o => o.Creator);
                    break;
                case "Creation_Date_field":
                    data = data.OrderByDirection(state.SortDirection, o => o.CreationDate);
                    break;
            }

            pagedData = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();
            return new TableData<Question>() { TotalItems = totalItems, Items = pagedData };

        }

        private async Task OnSearch(string text)
        {
            QuestionsDataToDisplay = QuestionsData.Where(q => q.QuestionBody.Contains(text) || q.Creator.ToLower().Contains(text.ToLower()));
            //searchString = text;
            //table.ReloadServerData();
        }


       
            protected async Task DeleteQuestion(int id)
            {
            QuestionsToDelete = await QuestionDataService.GetQuestionByIdAsync(id);
            await QuestionDataService.DeleteQuestion(QuestionsToDelete);
            await QuestionDataService.DeleteQuestionConnection(id);
            QuestionsData = await QuestionDataService.GetQuestionsAsync();
            QuestionsDataToDisplay = QuestionsData;

            NavigationManager.NavigateTo("./Questions");




            }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}



//data = data.Where(question =>

//{
//    if (string.IsNullOrWhiteSpace(searchString))
//        return true;
//    if (question.Creator.Contains(searchString, StringComparison.OrdinalIgnoreCase))
//        return true;
//    if (question.QuestionBody.Contains(searchString, StringComparison.OrdinalIgnoreCase))
//        return true;
//    if ($"{question.Difficulty} {question.Type} {question.UpdateDate}".Contains(searchString))
//        return true;
//    return false;
//}).ToArray();
//totalItems = data.Count();