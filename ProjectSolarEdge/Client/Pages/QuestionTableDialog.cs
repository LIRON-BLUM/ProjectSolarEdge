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
                //GameCRUD = new Game { CreationDate = DateTime.Now, UpdateDate = DateTime.Now, GameTheme = (GameTheme)1, GameStartDate = DateTime.Now, GameEndDate = DateTime.Now, CreatorID = 1, GameTimeLimit = 10, ScoreMethod = (ScoreMethod)1, ScoreEasy = 200, ScoreMedium = 300, ScoreHard = 400, IsGamified = 1, WheelIteration = 1, GambleIteration = 1 };
                GameCRUD.Questions = new List<Question>();
                QuestionsNotInGame = await QuestionDataService.GetQuestionsAsync();
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

        //protected async Task GamificationTrue()
        //{

        //    GameCRUD.IsGamified = 1;
        //}

        //protected async Task GamificationFalse()
        //{

        //    GameCRUD.IsGamified = 0;
        //}


        //protected async Task SaveGame()
        //{


        //    NavigationDestination = 1;
        //    AddAndUpdate();
        //}



        protected async Task SaveQuestions()
        {



            if (GameCRUD.ID == 0) // Create new question
            {




                MudDialog.Close(DialogResult.Ok(selectedQuestions));

            }
            else
            {


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

                await GameDataService.UpdateGame(GameCRUD);

            }



            ////ReloadGameID = gameId;

            MudDialog.Close(DialogResult.Ok(selectedQuestions));
       
        }
         
    

        protected async Task AddAndUpdate()
        {

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
        private IEnumerable<Question> pagedData;
        private MudTable<Question> table;
        public string SubName = "";

        //private bool FilterFunc(Game Game)





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
                case "Subject_field":
                    data = data.OrderByDirection(state.SortDirection, o => o.Subjects);
                    break;
            }

            pagedData = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();
            return new TableData<Question>() { TotalItems = totalItems, Items = pagedData };

        }



    }
}
