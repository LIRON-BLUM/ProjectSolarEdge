using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ProjectSolarEdge.Client.Services.Questions;
using ProjectSolarEdge.Shared.Entities;
using MudBlazor;
using ProjectSolarEdge.Client.Services.Games;

namespace ProjectSolarEdge.Client.Pages
{
    public partial class EditGames : ComponentBase, IDisposable
    {
        [Parameter]
        public string Id { get; set; }

        public Game GameCRUD { get; set; } = new Game();

        public IEnumerable<Game> GameData { get; set; }

        public IEnumerable<Question> QuestionsData { get; set; }

        public HashSet<Question> selectedQuestions = new HashSet<Question>();

        public IEnumerable<Question> Elements = new List<Question>();

        [Inject]
        public IGamesDataService GameDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IQuestionsDataService QuestionDataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
          
            GameCRUD = await GameDataService.GetGameByIdAsync(int.Parse(Id));
            QuestionsData = await QuestionDataService.GetQuestionsAsync();
           


            //selectedQuestions = new HashSet<Question>((IEnumerable<Question>)GameCRUD.Questions.Select(q => q.ID));
        }

        bool fixed_header = true;
        bool fixed_footer = false;

        //private string searchString = "";
        private string searchString = "";
        private int totalItems;
        private IEnumerable<Game> pagedData;
        private MudTable<Game> table;
        public string SubName = "";

        //private bool FilterFunc(Game Game)

        protected async Task<TableData<Game>> ServerReload(TableState state)
        {

            IEnumerable<Game> data = await GameDataService.GetAllGames();
            //await Task.Delay(300);
            data = data.Where(Game =>

            {
                if (string.IsNullOrWhiteSpace(searchString))
                    return true;
                if (Game.GameName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;

                if ($"{Game.GameTimeLimit} {Game.UpdateDate}".Contains(searchString))
                    return true;
                return false;
            }).ToArray();
            totalItems = data.Count();


            pagedData = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();
            return new TableData<Game>() { TotalItems = totalItems, Items = pagedData };

        }

        protected async Task AddAndUpdate()
        {

            await GameDataService.UpdateGame(GameCRUD);
            NavigationManager.NavigateTo("/Games");
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        // 1) Check which answer is the correct one and set it up
        //foreach (var ans in GameCRUD.Questions)
        //{

        //    ans.IsRight = false;

        //    if (GameCRUD.Questions.Where(a => a.ID == CheckedQuestionID).Count() > 0)
        //    {
        //        GameCRUD.Questions.Where(ans => ans.ID == CheckedQuestionID).FirstOrDefault().IsRight = true;
        //    }
        //}


        //if (QuestionsCRUD.ID == 0) // Create new question
        //{
        //    // 2) Save the question itself into the database and get the question ID back from the database
        //    int QuestionID = await QuestionDataService.AddQuestionToDB(QuestionsCRUD);

        //    if (QuestionID != 0) // Question added to the DB
        //    {
        //        QuestionsCRUD.ID = QuestionID;
        //    }

        //    // 3) Add the answers on the question to the database using the question ID retunred from the DB 
        //    foreach (var ans in QuestionsCRUD.Answers)
        //    {
        //        ans.QuestionID = QuestionID;
        //        await QuestionDataService.AddAnswerToDB(ans);

        //    }

        //    //4) If all successful then navigate the user to edit question or list of questions.
        //    NavigationManager.NavigateTo("/");

        //}
        //else
        //{
        //    //  QuestionsCRUD.Answers = new List<QuestionAnswer>();

        //    await QuestionDataService.UpdateQuestion(QuestionsCRUD);

        //    foreach (var ans in QuestionsCRUD.Answers)
        //    {

        //        await QuestionDataService.UpdateAnswer(ans);


        //  }  }



    }

}

