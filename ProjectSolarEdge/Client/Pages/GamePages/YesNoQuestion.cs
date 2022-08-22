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
        public string GameId { get; set; }

        [Parameter]
        public string UserId { get; set; }

        [Parameter]
        public string QuestionId { get; set; }

        public Game GamePlaying { get; set; }

        public UsersTable Player { get; set; }

        public Question currentQuestion { get; set; }

        public GameQuestionsConnection questionScore { get; set; }
        public string chosenanswer { get; set; }
        public IEnumerable<Question> availleblQuestions { get; set; }

        public GameScore questionScoreToInsert { get; set; }

        int currentScore = 200;
        public int currentQuestionNum { get; set; }

        [Inject]
        public IGamesDataService GameDataService { get; set; }

        [Inject]
        public IGameAppService GameAppDataService { get; set; }

        [Inject]
        public IQuestionsDataService QuestionDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }



        protected override async Task OnInitializedAsync()
        {
            GamePlaying = await GameDataService.GetGameByIdAsync(int.Parse(GameId));
            Player = await GameAppDataService.GetPlayerByID(int.Parse(UserId));
     

            currentQuestion = await QuestionDataService.GetQuestionByIdAsync(int.Parse(QuestionId));


            questionScore = new GameQuestionsConnection()
            {
                Score = 200
            };

            int.TryParse(GameId, out var gameId);
            int.TryParse(UserId, out var userId);
            availleblQuestions = await GameAppDataService.AvailableQuestions(gameId, userId);

            currentQuestionNum = GamePlaying.Questions.Count() - availleblQuestions.Count();

        }


        protected async Task saveAnawer()
        {
            //  Liron check if the is a row for this question in gameScore table if ther is Update the game score table with this if not insert

            questionScoreToInsert = new GameScore()
            {
                UserID = int.Parse(UserId),
                GameID = int.Parse(GameId),
                QuestionID = int.Parse(QuestionId),
                GameElement = 2,
                //In the DB IsRight id bit not bool
                //IsRight = Convert.ToBoolean(chosenanswer),
                IsRight = true,
                ElementScore = questionScore.Score,
                IsAnswered = true
            };

            await GameAppDataService.UpdateScoreElement(questionScoreToInsert);
            NavigationManager.NavigateTo($"./GetNextStep/{GameId}/{UserId}");

        }

        protected async Task SkipAnawer()
        {
            NavigationManager.NavigateTo($"./GetNextStep/{GameId}/{UserId}");

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

