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

namespace ProjectSolarEdge.Client.Pages
{
  
    public partial class Questions
    {
        public Question QuestionsCRUD { get; set; } = new Question();

        public Question QuestionsToDelete { get; set; } = new Question();

        public QuestionAnswer Answer { get; set; } = new QuestionAnswer();
        public IEnumerable<Question> QuestionsData { get; set; }

        [Inject]
        public IQuestionsDataService QuestionsDataService { get; set; }

        [Inject]
        public IQuestionsDataService QuestionDataService { get; set; }


        [Inject]
        public NavigationManager NavigationManager { get; set; }
       
        protected override async Task OnInitializedAsync()
        {
          QuestionsData = await QuestionDataService.GetQuestionsAsync();
            //  QuestionsData = await httpClient.GetFromJsonAsync<List<Element>>("webapi/periodictable");

        }

        protected async Task NewQuestion()
        {
           
            NavigationManager.NavigateTo("/EditQuestion");

        }

        protected async Task NavigateToEdit(int qid)
        {
            if (qid == 4) { 
            NavigationManager.NavigateTo("/EditQuestion/" + qid);
            }
        }



        bool fixed_header = true;
        bool fixed_footer = false;

        //private string searchString = "";
        private string searchString = "";
        private int totalItems;
        private IEnumerable<Question> pagedData;
        private MudTable<Question> table;


        //private bool FilterFunc(Question question)

        protected async Task<TableData<Question>> ServerReload(TableState state)
        {

            IEnumerable<Question> data = await QuestionDataService.GetQuestionsAsync();
            //await Task.Delay(300);
            data = data.Where(question =>

            {
                if (string.IsNullOrWhiteSpace(searchString))
                    return true;
                if (question.Creator.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (question.QuestionBody.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if ($"{question.Difficulty} {question.Type} {question.UpdateDate}".Contains(searchString))
                    return true;
                return false;
            }).ToArray();
            totalItems = data.Count();
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
            searchString = text;
            table.ReloadServerData();
        }


        protected async Task QuestionsToDeleteID(int id)
        {
            QuestionsToDelete = await QuestionDataService.GetQuestionByIdAsync(id);

            DeleteQuestion();

        }
            protected async Task DeleteQuestion()
            {

                await QuestionDataService.DeleteQuestion(QuestionsToDelete);


                //foreach (var ans in QuestionsToDelete.Answers)
                //{


                //    await QuestionDataService.DeleteAnswer(ans);
                //    //await QuestionDataService.DeleteQuestionAnswers(ans.QuestionID);

                //}

                //NavigationManager.NavigateTo("/");


            }





       

    }
}
