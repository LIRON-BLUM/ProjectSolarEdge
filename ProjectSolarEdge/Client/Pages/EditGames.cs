using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ProjectSolarEdge.Client.Services.Questions;
using ProjectSolarEdge.Shared.Entities;
using MudBlazor;
using ProjectSolarEdge.Client.Services.Games;
using ProjectSolarEdge.Client.Shared;

namespace ProjectSolarEdge.Client.Pages
{
    public partial class EditGames : ComponentBase, IDisposable
    {
        [CascadingParameter]
        public MainLayout MainLayout { get; set; }

        [CascadingParameter]
        public NavMenu NavLayout { get; set; }

        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public string EditorID { get; set; }

        public int NavigationDestination { get; set; }
        public Game GameCRUD { get; set; } = new Game();
      
        public IEnumerable<Game> GameData { get; set; }

        public IEnumerable<Question> QuestionsData { get; set; }

        public Question QuestionsToDelete { get; set; } = new Question();

        public bool Gamification = true;

        public IEnumerable<Question> QuestionsDataToDisplay { get; set; }



        //public IEnumerable<Question> selectedQuestions { get; set; } = new HashSet<Question>();

        public HashSet<Question> selectedQuestions { get; set; } = new HashSet<Question>();

      
        public IEnumerable<Question> Elements = new List<Question>();

        public string DefaultValue { get; set; } = "Select Question";



        //public IEnumerable<string> SelectedSubjects { get; set; } = new HashSet<string>();

        public List<GameQuestionsConnection> GameQuestionsConnectionsData { get; set; }


        public GamesQuestions GameQuestion { get; set; } = new GamesQuestions();

        public IEnumerable<GamesQuestions> GameQuestionData { get; set; }

 

        [Inject]
        public IGamesDataService GameDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IQuestionsDataService QuestionDataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            QuestionsData = await QuestionDataService.GetQuestionsAsync();
            int.TryParse(Id, out var GId);

            if (GId == 0)
            {
              GameCRUD = new Game { CreationDate = DateTime.Now, UpdateDate = DateTime.Now, GameTheme = (GameTheme)1, GameStartDate = DateTime.Now, GameEndDate = DateTime.Now, CreatorID = 1,GameTimeLimit=10, ScoreMethod = (ScoreMethod)1, ScoreEasy = 200, ScoreMedium = 300, ScoreHard = 400, IsGamified = 1, WheelIteration = 1, GambleIteration = 1 };
              GameCRUD.Questions = new List<Question>();
            } else
            {
                GameCRUD = await GameDataService.GetGameByIdAsync(int.Parse(Id));

                selectedQuestions = GameCRUD.Questions.ToHashSet();


            }

          
            QuestionsData = await QuestionDataService.GetQuestionsAsync();
            Elements = QuestionsData;


        }

        protected async Task DeleteQuestionFromGame(int Qid)
        {
            QuestionsToDelete = await QuestionDataService.GetQuestionByIdAsync(Qid);
          
            await GameDataService.DeleteQuestionIDConnction(Qid);
            QuestionsData = await QuestionDataService.GetQuestionsAsync();
            QuestionsDataToDisplay = QuestionsData;

            int.TryParse(Id, out var GId);
            //NavigationManager.NavigateTo($"/EditGame/{GId}");
        }




        protected async Task OpenDialog(int GameID)
        {
            int.TryParse(Id, out var GId);
            var options = new DialogOptions { };
            var parameters = new DialogParameters();
            
            parameters.Add("GameID", GId);
            parameters.Add("ReloadGameID", GameID);
            var dialog = DialogService.Show<QuestionTableDialog>("Simple Dialog", parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                NavigationManager.NavigateTo($"/EditGame/{GId}");
            }
        }

        protected async Task GamificationTrue()
        {
            
            GameCRUD.IsGamified = 1;
        }

        protected async Task GamificationFalse()
        {
            
            GameCRUD.IsGamified = 0;
        }


        protected async Task SaveGame()
        {
          

            NavigationDestination = 1;
             AddAndUpdate();
        }

   

        protected async Task AddAndUpdate()
        {

      

   
            


            //GameCRUD.Questions = selectedQuestionToUpdate;


            if (GameCRUD.ID == 0) // Create new question
            {



                 // 2) Save the question itself into the database and get the question ID back from the database
                 await GameDataService.AddGameToDB(GameCRUD);

              

                }
            else
            {
              await GameDataService.UpdateGame(GameCRUD);
             
            }

         
            NavigationManager.NavigateTo("/Games");
        }

   

  

        protected async Task Navigation(int pageNum)
        {
            if (pageNum == 1)
            {
                NavigationManager.NavigateTo("/Games");
            } else
            NavigationManager.NavigateTo($"/EditGame/{Id}");
        }



     

      


        public void Dispose()
        {
            throw new NotImplementedException();
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

        //protected async Task<TableData<Game>> ServerReload(TableState state)
        //{

        //    IEnumerable<Game> data = await GameDataService.GetAllGames();
        //    //await Task.Delay(300);
        //    data = data.Where(Game =>

        //    {
        //        if (string.IsNullOrWhiteSpace(searchString))
        //            return true;
        //        if (Game.GameName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
        //            return true;

        //        if ($"{Game.GameTimeLimit} {Game.UpdateDate}".Contains(searchString))
        //            return true;
        //        return false;
        //    }).ToArray();
        //    totalItems = data.Count();


        //    pagedData = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();
        //    return new TableData<Game>() { TotalItems = totalItems, Items = pagedData };

        //}

     


    }

}

