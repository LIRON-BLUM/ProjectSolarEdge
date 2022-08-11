using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ProjectSolarEdge.Client.Services.Questions;
using ProjectSolarEdge.Shared.Entities;
using MudBlazor;
using ProjectSolarEdge.Client.Services.Games;


namespace ProjectSolarEdge.Client.Pages

{
    public partial class QuestionTableDialog : ComponentBase, IDisposable
    {

        [Parameter]
        public string Id { get; set; }

        [Parameter] public int GameID { get; set; }

        [Parameter] public int ReloadGameID { get; set; }

        public int NavigationDestination { get; set; }
        public Game GameCRUD { get; set; } = new Game();

        public IEnumerable<Game> GameData { get; set; }

        public IEnumerable<Question> QuestionsData { get; set; }

        public Question QuestionsToDelete { get; set; } = new Question();

        public bool Gamification = true;

        public IEnumerable<Question> QuestionsDataToDisplay { get; set; }

        //public IEnumerable<Question> selectedQuestions { get; set; } = new HashSet<Question>();

        public HashSet<Question> selectedQuestions { get; set; } = new HashSet<Question>();


        public IEnumerable<Question> QuestionsNotInGame = new List<Question>();

        public string DefaultValue { get; set; } = "Select Question";



        //public IEnumerable<string> SelectedSubjects { get; set; } = new HashSet<string>();

        public List<GameQuestionsConnection> GameQuestionsConnectionsData { get; set; }


        public GamesQuestions GameQuestion { get; set; } = new GamesQuestions();

        public IEnumerable<GamesQuestions> GameQuestionData { get; set; }
        public GameQuestionsConnection questionToUpdate { get; set; }


        [Inject]
        public IGamesDataService GameDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IQuestionsDataService QuestionDataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            QuestionsData = await QuestionDataService.GetQuestionsAsync();
            //int.TryParse(Id, out var GId);

            if (GameID == 0)
            {
                GameCRUD = new Game { CreationDate = DateTime.Now, UpdateDate = DateTime.Now, GameTheme = (GameTheme)1, GameStartDate = DateTime.Now, GameEndDate = DateTime.Now, CreatorID = 1, GameTimeLimit = 10, ScoreMethod = (ScoreMethod)1, ScoreEasy = 200, ScoreMedium = 300, ScoreHard = 400, IsGamified = 1, WheelIteration = 1, GambleIteration = 1 };
                GameCRUD.Questions = new List<Question>();
            }
            else
            {
                GameCRUD = await GameDataService.GetGameByIdAsync(GameID);

                //selectedQuestions = GameCRUD.Questions.ToHashSet();
                QuestionsNotInGame = await QuestionDataService.GetQuestionsThatNotInGameID(GameID);
                //QuestionsNotInGame = QuestionsData;

            }


            

   



            //QuestionsData = await QuestionDataService.GetQuestionsAsync();
            //Elements = QuestionsData;

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



        protected async Task SaveQuestions()
        {

            //int gameId = GameID;

            //List<Question> selectedQuestionToUpdate = new List<Question>();




            //// Delete the existing subjects 
            //await GameDataService.DeleteQuestionConnection(gameId);

            //foreach (var item in selectedQuestions)
            //{

            //    int QuestionScore = 0;
            //    Question q = QuestionsData.Where(q => q.ID == item.ID).SingleOrDefault();
            //    selectedQuestionToUpdate.Add(q);

            //    if (item.Difficulty == QuestionDifficulty.Easy)
            //    {
            //        QuestionScore = 200;
            //    }
            //    if (item.Difficulty == QuestionDifficulty.Medium)
            //    {
            //        QuestionScore = 400;
            //    }
            //    if (item.Difficulty == QuestionDifficulty.Hard)
            //    {
            //        QuestionScore = 600;
            //    }



            //    questionToUpdate = new GameQuestionsConnection()
            //    {
            //        QuestionID = q.ID,
            //        GameID = gameId,
            //        Score = QuestionScore
            //    };


            //    await GameDataService.AddQuestionConnection(questionToUpdate);
            //}

            int gameId = GameID;

            List<Question> selectedQuestionToUpdate = new List<Question>();


            selectedQuestionToUpdate = GameCRUD.Questions;

            //// Delete the existing subjects 
            //await GameDataService.DeleteQuestionConnection(gameId);

            foreach (var item in selectedQuestions)
            {

                int QuestionScore = 0;
                Question q = QuestionsData.Where(q => q.ID == item.ID).SingleOrDefault();


                if (q.Difficulty == QuestionDifficulty.Easy)
                {
                    QuestionScore = 200;
                }
                if (q.Difficulty == QuestionDifficulty.Medium)
                {
                    QuestionScore = 400;
                }
                if (q.Difficulty == QuestionDifficulty.Hard)
                {
                    QuestionScore = 600;
                }

                selectedQuestionToUpdate.Add(q);
                //await GameDataService.AddQuestionConnection(new GameQuestionsConnection() { QuestionID = q.ID, GameID = gameId, Score = QuestionScore });

                questionToUpdate = new GameQuestionsConnection()
                {
                    QuestionID = q.ID,
                    GameID = gameId,
                    Score = QuestionScore
                };

                await GameDataService.AddQuestionConnection(questionToUpdate);
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

            NavigationDestination = 0;

            ReloadGameID = gameId;
            //AddAndUpdate();

            //MudDialog.Close(DialogResult.Ok(true));
            MudDialog.Close(DialogResult.Ok(gameId));
        //    NavigationManager.NavigateTo($"/EditGame/{GameID}");
        }
         
    

        protected async Task AddAndUpdate()
        {

        //    int gameId = GameID;

        //    List<Question> selectedQuestionToUpdate = new List<Question>();


        //    selectedQuestionToUpdate = GameCRUD.Questions;

        //    //// Delete the existing subjects 
        //    //await GameDataService.DeleteQuestionConnection(gameId);

        //    foreach (var item in selectedQuestions)
        //    {

        //        int QuestionScore = 0;
        //        Question q = QuestionsData.Where(q => q.ID == item.ID).SingleOrDefault();
               

        //        if (q.Difficulty == QuestionDifficulty.Easy)
        //        {
        //            QuestionScore = 200;
        //        }
        //        if (q.Difficulty == QuestionDifficulty.Medium)
        //        {
        //            QuestionScore = 400;
        //        }
        //        if (q.Difficulty == QuestionDifficulty.Hard)
        //        {
        //            QuestionScore = 600;
        //        }
                
        //        selectedQuestionToUpdate.Add(q);
        //        //await GameDataService.AddQuestionConnection(new GameQuestionsConnection() { QuestionID = q.ID, GameID = gameId, Score = QuestionScore });

        //        questionToUpdate = new GameQuestionsConnection()
        //        {
        //            QuestionID = q.ID,
        //            GameID = gameId,
        //            Score = QuestionScore
        //        };

        //        await GameDataService.AddQuestionConnection(questionToUpdate);
        //    }



          
        


        //GameCRUD.Questions = selectedQuestionToUpdate;


        //    if (GameCRUD.ID == 0) // Create new question
        //    {



        //        // 2) Save the question itself into the database and get the question ID back from the database
        //        await GameDataService.AddGameToDB(GameCRUD);



        //    }
        //    else
        //    {
        //        await GameDataService.UpdateGame(GameCRUD);

        //    }

            
            //Navigation(NavigationDestination);
            //  NavigationManager.NavigateTo("/Games");
        }

        protected async Task DeleteQuestion(int id)
        {
            QuestionsToDelete = await QuestionDataService.GetQuestionByIdAsync(id);
            await QuestionDataService.DeleteQuestion(QuestionsToDelete);

            QuestionsData = await QuestionDataService.GetQuestionsAsync();
            QuestionsDataToDisplay = QuestionsData;


            NavigationManager.NavigateTo($"/EditGame/{GameID}");


        }




        protected async Task Navigation(int pageNum)
        {
            if (pageNum == 1)
            {
                NavigationManager.NavigateTo("/Games");
            }
            else
                NavigationManager.NavigateTo($"/EditGame/{GameID}");
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
