using Microsoft.AspNetCore.Components;
using ProjectSolarEdge.Client.Services.GameApp;
using ProjectSolarEdge.Client.Services.Games;
using ProjectSolarEdge.Client.Services.Questions;
using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Client.Pages.GamePages
{
    public partial class MultipelQuestion
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

        public GameScore questionScoreToInsert { get; set; }
        public IEnumerable<GameScore> gameCurrentScore { get; set; }

        public UserGameScore currentScore { get; set; }

        public int CorrentScoreToInsert { get; set; }

        public IEnumerable<Question> availleblQuestions { get; set; }

        [Inject]
        public IQuestionsDataService QuestionDataService { get; set; }

        [Inject]
        public IGamesDataService GameDataService { get; set; }

        [Inject]
        public IGameAppService GameAppDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }



        public int currentQuestionNum { get; set; }


        protected override async Task OnInitializedAsync()
        {
            int.TryParse(GameId, out var gameId);
            int.TryParse(UserId, out var userId);

            GamePlaying = await GameDataService.GetGameByIdAsync(int.Parse(GameId));


            Player = await GameAppDataService.GetPlayerByID(int.Parse(UserId));


            currentQuestion = await QuestionDataService.GetQuestionByIdAsync(int.Parse(QuestionId));

            currentScore = await GameAppDataService.GetGameUserScoreByUserID(gameId, userId);

            questionScore = new GameQuestionsConnection()
            {
                Score = 200
            };



            availleblQuestions = await GameAppDataService.AvailableQuestions(gameId, userId);

            //availleblQuestions = new List<Question>()
            //{
            //    new Question()
            //    {
            //        ID = 1,
            //        Type= QuestionType.SingleChoice
            //    },
            //      new Question() {
            //        ID = 2,
            //        Type= QuestionType.TrueFalse
            //    },


            //};

            gameCurrentScore = new List<GameScore>()
            {
                new GameScore()
                {
                    ElementScore= 100
                },
                 new GameScore()
                {
                    ElementScore= 200
                },
                  new GameScore()
                {
                    ElementScore= 400
                },
                   new GameScore()
                {
                    ElementScore= 100
                }
            };


            //foreach (int score in gameCurrentScore)
            //{
            //    currentScore += score.;
            //}


            currentQuestionNum = GamePlaying.Questions.Count() - availleblQuestions.Count();



        }


        protected async Task saveAnawer()
        {
            //  Liron check if the is a row for this question in gameScore table if ther is Update the game score table with this if not insert
            //is the Query supposed to be - update Game score table?

          

            if (chosenanswer == "true" || chosenanswer == "True")
            {
                CorrentScoreToInsert = questionScore.Score;
            }
            else
            {
                CorrentScoreToInsert = 0 - questionScoreToInsert.GamblingScore;
            }


            questionScoreToInsert = new GameScore()
            {
                UserID = int.Parse(UserId),
                GameID = int.Parse(GameId),
                QuestionID = int.Parse(QuestionId),

                //In the DB IsRight id bit not bool
                IsRight = Convert.ToBoolean(chosenanswer),
                ElementScore = CorrentScoreToInsert,
                IsAnswered = true
            };

            await GameAppDataService.UpdateScoreElement(questionScoreToInsert);

            NavigationManager.NavigateTo($"GetNextStep/{GameId}/{UserId}");

        }

        protected async Task SkipAnawer()
        {
            NavigationManager.NavigateTo($"GetNextStep/{GameId}/{UserId}");

        }

    }
}


