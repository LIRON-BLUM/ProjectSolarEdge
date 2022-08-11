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
            await QuestionDataService.DeleteQuestion(QuestionsToDelete);
            await GameDataService.DeleteQuestionIDConnction(Qid);
            QuestionsData = await QuestionDataService.GetQuestionsAsync();
            QuestionsDataToDisplay = QuestionsData;

            int.TryParse(Id, out var GId);
            NavigationManager.NavigateTo($"/EditGame/{GId}");
        }


  

        protected void OpenDialog()
        {
            int.TryParse(Id, out var GId);
            var options = new DialogOptions { };
            var parameters = new DialogParameters();
            parameters.Add("GameID", GId);
            DialogService.Show<QuestionTableDialog>("Simple Dialog", parameters, options);
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

            int.TryParse(Id, out var GId);

            List<Question> selectedQuestionToUpdate = new List<Question>();


            // Delete the existing subjects 
            await GameDataService.DeleteQuestionConnection(GId);

            foreach (var item in selectedQuestions)
            {

                int QuestionScore = 0;
                Question q = QuestionsData.Where(q => q.ID == item.ID).SingleOrDefault();
                selectedQuestionToUpdate.Add(q);

                if (item.Difficulty == QuestionDifficulty.Easy)
                {
                    QuestionScore = 200;
                }
                if (item.Difficulty == QuestionDifficulty.Medium)
                {
                    QuestionScore = 400;
                } if (item.Difficulty == QuestionDifficulty.Hard)
                {
                    QuestionScore = 600;
                }

                await GameDataService.AddQuestionConnection(new GameQuestionsConnection() { QuestionID = q.ID, GameID = GId, Score= QuestionScore });

            }
            


            GameCRUD.Questions = selectedQuestionToUpdate;


            if (GameCRUD.ID == 0) // Create new question
            {



                 // 2) Save the question itself into the database and get the question ID back from the database
                 await GameDataService.AddGameToDB(GameCRUD);

              

                }
            else
            {
              await GameDataService.UpdateGame(GameCRUD);
             
            }

            Navigation(NavigationDestination);
            //  NavigationManager.NavigateTo("/Games");
        }

        //protected async Task DeleteQuestion(int id)
        //{
        //    QuestionsToDelete = await QuestionDataService.GetQuestionByIdAsync(id);
        //    await QuestionDataService.DeleteQuestion(QuestionsToDelete);

        //    QuestionsData = await QuestionDataService.GetQuestionsAsync();
        //    QuestionsDataToDisplay = QuestionsData;


        //    NavigationManager.NavigateTo($"/EditGame/{Id}");


        //}


        protected async Task Navigation(int pageNum)
        {
            if (pageNum == 1)
            {
                NavigationManager.NavigateTo("/Games");
            } else
            NavigationManager.NavigateTo($"/EditGame/{Id}");
        }



        public bool _isOpen = false;

        public void ToggleOpen()
        {
            if (_isOpen)
            {
                _isOpen = false;



               
               
                //NavigationManager.NavigateTo($"/EditGame/{Id}");
            }
            //NavigationManager.NavigateTo($"/EditGame/{Id}");
            else
            {
                _isOpen = true;
                NavigationDestination = 2;
                AddAndUpdate();
            }
                

           

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

     


    }

}

