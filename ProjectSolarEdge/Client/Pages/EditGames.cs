using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ProjectSolarEdge.Client.Services.Questions;
using ProjectSolarEdge.Shared.Entities;
using MudBlazor;
using ProjectSolarEdge.Client.Services.Games;
using ProjectSolarEdge.Client.Shared;
using ProjectSolarEdge.Client.Services.Users;

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

        public UsersTable EditorData { get; set; }

        public string CreatorName = " ";

        [Inject]
        public IGamesDataService GameDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IQuestionsDataService QuestionDataService { get; set; }

        [Inject]
        public IUsersDataService UsersDataService { get; set; }

        [Inject]
        public Blazored.LocalStorage.ISyncLocalStorageService LocalService { get; set; }

        string EditorIDSessiom = "";

        public List<Question> selectedQuestionToUpdate = new List<Question>();


        protected override async Task OnInitializedAsync()
        {
            EditorIDSessiom = LocalService.GetItem<string>("SessionValue");

            int.TryParse(EditorIDSessiom, out var EId);
            int.TryParse(Id, out var GId);
            EditorData = await UsersDataService.GetUsererByID(EId);



            CreatorName = (EditorData.UserFirstName + " " + EditorData.UserLastName);

            QuestionsData = await QuestionDataService.GetQuestionsAsync();
           
           

            if (GId == 0)
            {
                //get all questions in the dialog 
              GameCRUD = new Game { CreationDate = DateTime.Now, UpdateDate = DateTime.Now, GameTheme = (GameTheme)1, GameStartDate = DateTime.Now, GameEndDate = DateTime.Now, CreatorID = 1,GameTimeLimit=10, ScoreMethod = (ScoreMethod)1, ScoreEasy = 200, ScoreMedium = 300, ScoreHard = 400, IsGamified = 1, WheelIteration = 1, GambleIteration = 1, Creator = CreatorName };
              GameCRUD.Questions = new List<Question>();
                foreach (var Ques in GameCRUD.Questions)
                {
                    Ques.Answers = new List<QuestionAnswer>();
                }
            } else
            {
                GameCRUD = await GameDataService.GetGameByIdAsync(int.Parse(Id));

                selectedQuestions = GameCRUD.Questions.ToHashSet();
                QuestionsDataToDisplay = GameCRUD.Questions;

               // GameCRUD.Questions = selectedQuestionToUpdate;
            }

          
            QuestionsData = await QuestionDataService.GetQuestionsAsync();
            Elements = QuestionsData;


        }

        protected async Task DeleteQuestionFromGame(int Qid)
        {
            int.TryParse(Id, out var GId);
            QuestionsToDelete = await QuestionDataService.GetQuestionByIdAsync(Qid);
            Question QuesToDelete = await QuestionDataService.GetQuestionByIdAsync(Qid);
            await GameDataService.DeleteQuestionIDConnction(Qid, GId);
            //QuestionsData = await QuestionDataService.GetQuestionsAsync();
            GameCRUD = await GameDataService.GetGameByIdAsync(int.Parse(Id));          
            QuestionsDataToDisplay = GameCRUD.Questions;
        }




        protected async Task OpenDialog(int GameID)
        {
            OpenOverlay();
            int.TryParse(Id, out var GId);
            var options = new DialogOptions { };
            var parameters = new DialogParameters();
            
            parameters.Add("GameID", GId);
            parameters.Add("ReloadGameID", GameID);
            var dialog = DialogService.Show<QuestionTableDialog>("Simple Dialog", parameters, options);
            var result = await dialog.Result;

           // GameCRUD.Questions = result;

            if (!result.Cancelled)
            {
                IEnumerable<Question> q = (IEnumerable<Question>)result.Data;
                GameCRUD.Questions.AddRange(q);
           
                QuestionsDataToDisplay = GameCRUD.Questions;
                //QuestionsDataToDisplay = GameCRUD.Questions;
                //  GameCRUD.Questions = result;
                //NavigationManager.NavigateTo($"/EditGame/{GId}");
            }
        }

        private bool isVisible;

        public void OpenOverlay()
        {
            isVisible = true;
            StateHasChanged();
        }


        protected async Task GamificationTrue()
        {
            
            GameCRUD.IsGamified = 1;
        }

        protected async Task GamificationFalse()
        {
            
            GameCRUD.IsGamified = 0;
        }

        public Game GameToDelete { get; set; } = new Game();
        protected async Task DeleteGame()
        {
            await GameDataService.DeleteGame(GameCRUD);
            NavigationManager.NavigateTo("./Games");
            //DeleteGame();

        }


        public GameQuestionsConnection questionToUpdate { get; set; }
        int GameIdToAdd { get; set; }

        public int QuestionScore = 200;

        protected async Task AddAndUpdate()
        {

            //  GameCRUD.Questions = selectedQuestionToUpdate;

            int.TryParse(Id, out var gameId);

          

            List<Question> selectedQuestionToUpdate = new List<Question>();


           

            if (GameCRUD.ID == 0) // Create new question
            {
                GameCRUD.Creator = CreatorName;
                //List<Question> selectedQuestionsToUpdate = new List<Question>();
                // 2) Save the question itself into the database and get the question ID back from the database
                GameIdToAdd =  await GameDataService.AddGameToDB(GameCRUD);

                //if (GameIdToAdd != 0) // Question added to the DB
                //{
                //    GameCRUD.ID = GameIdToAdd;
               

                  foreach (var item in QuestionsDataToDisplay)
                  {


                  
                       // int QuestionScore = 200;
                        Question q = QuestionsData.Where(q => q.ID == item.ID).SingleOrDefault();
                        Question newQ = await QuestionDataService.GetQuestionByIdAsync(q.ID);
                    if (GameCRUD.ScoreMethod == ScoreMethod.SpreadEqualy)
                    {
                        if (newQ.Difficulty == QuestionDifficulty.Easy)
                        {
                            QuestionScore = 200;
                        }
                        if (newQ.Difficulty == QuestionDifficulty.Medium)
                        {
                            QuestionScore = 200;
                        }
                        if (newQ.Difficulty == QuestionDifficulty.Hard)
                        {
                            QuestionScore = 200;
                        }
                    } else if (GameCRUD.ScoreMethod == ScoreMethod.SpreadByDifficulty)
                    {
                        if (newQ.Difficulty == QuestionDifficulty.Easy)
                        {
                           
                            QuestionScore = 200;
                        }
                        if (newQ.Difficulty == QuestionDifficulty.Medium)
                        {
                            QuestionScore = QuestionScore + 200;
                        }
                        if (newQ.Difficulty == QuestionDifficulty.Hard)
                        {
                            QuestionScore = QuestionScore + 400;
                        }
                    } else
                    {
                        if (newQ.Difficulty == QuestionDifficulty.Easy)
                        {
                            QuestionScore = 200;
                        }
                        if (newQ.Difficulty == QuestionDifficulty.Medium)
                        {
                            QuestionScore = QuestionScore;
                        }
                        if (newQ.Difficulty == QuestionDifficulty.Hard)
                        {
                            QuestionScore = QuestionScore;
                        }
                    }

                       

                       
                        selectedQuestionToUpdate.Add(newQ);
                        //await GameDataService.AddQuestionConnection(new GameQuestionsConnection() { QuestionID = q.ID, GameID = gameId, Score = QuestionScore });

                        questionToUpdate = new GameQuestionsConnection()
                        {
                            QuestionID = newQ.ID,
                            GameID = GameIdToAdd,
                            Score = QuestionScore
                        };

                      //  GameCRUD.Questions = selectedQuestionToUpdate;

                        await GameDataService.AddQuestionConnection(questionToUpdate);
                  }
                //}
            }
            else
            {

                

              await GameDataService.UpdateGame(GameCRUD);
             
            }

         
            NavigationManager.NavigateTo("./Games");
        }

   

  

        protected async Task Navigation(int pageNum)
        {
            if (pageNum == 1)
            {
                NavigationManager.NavigateTo("./Games");
            } else
            NavigationManager.NavigateTo($"./EditGame/{Id}");
        }



     

      


        public void Dispose()
        {
            //throw new NotImplementedException();
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

