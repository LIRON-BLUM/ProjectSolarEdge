using Microsoft.AspNetCore.Components;
using ProjectSolarEdge.Client.Services.Questions;
using ProjectSolarEdge.Shared.Entities;
using static System.Net.WebRequestMethods;

namespace ProjectSolarEdge.Client.Pages
{
    public partial class Questions
    {
        public IEnumerable<Question> QuestionsData { get; set; }
        [Inject]
        public IQuestionsDataService QuestionsDataService { get; set; }

        

        protected override async Task OnInitializedAsync()
        {
          QuestionsData = await QuestionsDataService.GetQuestionsAsync();
          

        }


   

    }
}
