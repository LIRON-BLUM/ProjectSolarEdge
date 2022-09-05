using Microsoft.AspNetCore.Components;
using ProjectSolarEdge.Client.Services.GameApp;
using ProjectSolarEdge.Client.Services.Games;
using ProjectSolarEdge.Client.Services.Questions;
using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Client.Pages.GamePages
{
    public partial class YesNoQuestion : ComponentBase, IDisposable
    {

        [Parameter]

        public string GameID { get; set; }

        [Parameter]
        public string UserID { get; set; }

        [Parameter]
        public string QuestionID { get; set; }

        public Game GamePlaying { get; set; }

        public UsersTable Player { get; set; }

        public Question currentQuestion { get; set; }

        public GameQuestionsConnection questionScore { get; set; }
        public string chosenanswer { get; set; }

        public GameScore questionScoreToUpdate { get; set; }
        public IEnumerable<GameScore> gameCurrentScore { get; set; }

        public UserGameScore currentScore { get; set; }

        public int CorrentScoreToInsert { get; set; }

        public IEnumerable<Question> availleblQuestions { get; set; }

        public GameScore LastGamblingScore { get; set; }

        public string QuestionImage { get; set; }


        [Inject]
        public IQuestionsDataService QuestionDataService { get; set; }

        [Inject]
        public IGamesDataService GameDataService { get; set; }

        [Inject]
        public IGameAppService GameAppDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }



        public int currentQuestionNum { get; set; }

        public int currentQuestionProgressNum { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        protected override async Task OnInitializedAsync()
        {
            int.TryParse(GameID, out var gameId);
            int.TryParse(UserID, out var userId);
            int.TryParse(QuestionID, out var questionId);

            GamePlaying = await GameDataService.GetGameByIdAsync(gameId);


            Player = await GameAppDataService.GetPlayerByID(userId);


            currentQuestion = await QuestionDataService.GetQuestionByIdAsync(questionId);

            questionScore = await GameAppDataService.GetQuestionScoreByGameID(gameId, questionId);

            currentScore = await GameAppDataService.GetGameUserScoreByUserID(gameId, userId);

            //questionScore = new GameQuestionsConnection()
            //{
            //    Score = 200
            //};



            availleblQuestions = await GameAppDataService.AvailableQuestions(gameId, userId);

            LastGamblingScore = await GameAppDataService.GetGamblingScore(gameId, userId);

            //gameCurrentScore = new List<GameScore>()
            //{
            //    new GameScore()
            //    {
            //        ElementScore= 100
            //    },
            //     new GameScore()
            //    {
            //        ElementScore= 200
            //    },
            //      new GameScore()
            //    {
            //        ElementScore= 400
            //    },
            //       new GameScore()
            //    {
            //        ElementScore= 100
            //    }
            //};


            //foreach (int score in gameCurrentScore)
            //{
            //    currentScore += score.;
            //}


            currentQuestionNum = GamePlaying.Questions.Count() - availleblQuestions.Count() + 1;

            currentQuestionProgressNum = currentQuestionNum * 10;

            QuestionImage = "data:image/png;base64," + currentQuestion.QuestionImagePath;


        }

        //protected async Task btnSample_Click()
        //{
        //    //NavigationManager.NavigateTo($"GetNextStep/{GameID}/{UserID}");



        //}


        protected async Task saveAnawer()
        {



            if (chosenanswer != "False")
            {
                CorrentScoreToInsert = (questionScore.Score) + (LastGamblingScore.GamblingScore);
            }
            else
            {
                CorrentScoreToInsert = 0 - (LastGamblingScore.GamblingScore);
            }

            //CorrentScoreToInsert = questionScore.Score;
            //CorrentScoreToInsert = 0 - questionScoreToUpdate.GamblingScore;

            questionScoreToUpdate = new GameScore()
            {
                UserID = int.Parse(UserID),
                GameID = int.Parse(GameID),
                QuestionID = int.Parse(QuestionID),
                IsRight = Convert.ToBoolean(chosenanswer),
                GamblingScore = LastGamblingScore.GamblingScore,
                ElementScore = CorrentScoreToInsert,
                IsAnswered = true
            };

            await GameAppDataService.UpdateScoreElement(questionScoreToUpdate);

            NavigationManager.NavigateTo($"./GetNextStep/{GameID}/{UserID}");

        }

        protected async Task SkipAnawer()
        {
            NavigationManager.NavigateTo($"./GetNextStep/{GameID}/{UserID}");

        }
        //[Parameter]
        //public string GameId { get; set; }

        //[Parameter]
        //public string UserId { get; set; }

        //[Parameter]
        //public string QuestionId { get; set; }

        //public Game GamePlaying { get; set; }

        //public UsersTable Player { get; set; }

        //public Question currentQuestion { get; set; }

        //public GameQuestionsConnection questionScore { get; set; }
        //public string chosenanswer { get; set; }
        //public IEnumerable<Question> availleblQuestions { get; set; }

        //public GameScore questionScoreToInsert { get; set; }

        //int currentScore = 200;
        //public int currentQuestionNum { get; set; }

        //[Inject]
        //public IGamesDataService GameDataService { get; set; }

        //[Inject]
        //public IGameAppService GameAppDataService { get; set; }

        //[Inject]
        //public IQuestionsDataService QuestionDataService { get; set; }

        //[Inject]
        //public NavigationManager NavigationManager { get; set; }



        //protected override async Task OnInitializedAsync()
        //{
        //    GamePlaying = await GameDataService.GetGameByIdAsync(int.Parse(GameId));
        //    Player = await GameAppDataService.GetPlayerByID(int.Parse(UserId));


        //    currentQuestion = await QuestionDataService.GetQuestionByIdAsync(int.Parse(QuestionId));


        //    questionScore = new GameQuestionsConnection()
        //    {
        //        Score = 200
        //    };

        //    int.TryParse(GameId, out var gameId);
        //    int.TryParse(UserId, out var userId);
        //    availleblQuestions = await GameAppDataService.AvailableQuestions(gameId, userId);

        //    currentQuestionNum = GamePlaying.Questions.Count() - availleblQuestions.Count() + 1;

        //}


        //protected async Task saveAnawer()
        //{
        //    //  Liron check if the is a row for this question in gameScore table if ther is Update the game score table with this if not insert

        //    questionScoreToInsert = new GameScore()
        //    {
        //        UserID = int.Parse(UserId),
        //        GameID = int.Parse(GameId),
        //        QuestionID = int.Parse(QuestionId),
        //        GameElement = 2,
        //        //In the DB IsRight id bit not bool
        //        //IsRight = Convert.ToBoolean(chosenanswer),
        //        IsRight = true,
        //        ElementScore = questionScore.Score,
        //        IsAnswered = true
        //    };

        //    await GameAppDataService.UpdateScoreElement(questionScoreToInsert);
        //    NavigationManager.NavigateTo($"./GetNextStep/{GameId}/{UserId}");

        //}

        //protected async Task SkipAnawer()
        //{
        //    NavigationManager.NavigateTo($"./GetNextStep/{GameId}/{UserId}");

        //}

        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}
    }
}

