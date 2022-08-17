using Microsoft.AspNetCore.Components;
using ProjectSolarEdge.Client.Services.GameApp;
using ProjectSolarEdge.Client.Services.Games;
using ProjectSolarEdge.Client.Services.Questions;
using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Client.Pages.GamePages
{
    public partial class OrderQuestion
    {
        [Parameter]

        public string GameId { get; set; }

        [Parameter]
        public string UserId { get; set; }

        [Parameter]
        public string QuestionId { get; set; }

        public Game GamePlaying { get; set; }

        public UsersTable player { get; set; }

        public Question currentQuestion { get; set; }

        public GameQuestionsConnection questionScore { get; set; }
        public string chosenanswer { get; set; }

        public GameScore questionScoreToUpdate { get; set; }
        public IEnumerable<GameScore> gameCurrentScore { get; set; }

        public UserGameScore currentScore { get; set; }

        public int CorrentScoreToInsert { get; set; }

        public IEnumerable<Question> availleblQuestions { get; set; }

        public GameScore LastGamblingScore { get; set; }

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
            // Get question by id from question service

            int.TryParse(GameId, out var gameId);
            int.TryParse(UserId, out var userId);
            int.TryParse(QuestionId, out var questionId);

            GamePlaying = await GameDataService.GetGameByIdAsync(gameId);


            player = await GameAppDataService.GetPlayerByID(userId);


            currentQuestion = await QuestionDataService.GetQuestionByIdAsync(questionId);

            questionScore = await GameAppDataService.GetQuestionScoreByGameID(gameId, questionId);

            currentScore = await GameAppDataService.GetGameUserScoreByUserID(gameId, userId);

            //questionScore = new GameQuestionsConnection()
            //{
            //    Score = 200
            //};

            availleblQuestions = await GameAppDataService.AvailableQuestions(gameId, userId);

            LastGamblingScore = await GameAppDataService.GetGamblingScore(gameId, userId);


            //currentQuestion = new Question()
            //{
            //    QuestionBody = "Sort the list of names so the oldest person is on top",
            //    Difficulty = QuestionDifficulty.Medium,
            //    Answers = new List<QuestionAnswer>()
            //    {
            //        new QuestionAnswer() {
            //            AnswerBody= "Yotam Avrahami",
            //            QuestionOrder = 1
            //        },
            //        new QuestionAnswer() {
            //            AnswerBody= "Limor Avrahami",
            //             QuestionOrder=2

            //        },
            //        new QuestionAnswer() {
            //            AnswerBody= "Roei Ben-Zvi",
            //              QuestionOrder=3

            //        },
            //        new QuestionAnswer() {
            //            AnswerBody= "Liron Blum",
            //              QuestionOrder=4

            //        }

            //    }

            //};


            //foreach (int score in gameCurrentScore)
            //{
            //    currentScore += score.;
            //}


            currentQuestionNum = GamePlaying.Questions.Count() - availleblQuestions.Count();
        }

        protected async Task saveAnawer()
        {
            //  Liron check if the is a row for this question in gameScore table if ther is Update the game score table with this if not insert

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
                UserID = int.Parse(UserId),
                GameID = int.Parse(GameId),
                QuestionID = int.Parse(QuestionId),
                IsRight = Convert.ToBoolean(chosenanswer),
                GamblingScore = LastGamblingScore.GamblingScore,
                ElementScore = CorrentScoreToInsert,
                IsAnswered = true
            };

            await GameAppDataService.UpdateScoreElement(questionScoreToUpdate);

            NavigationManager.NavigateTo($"GetNextStep/{GameId}/{UserId}");

        }
        protected async Task SkipAnawer()
        {
            NavigationManager.NavigateTo($"GetNextStep/{GameId}/{UserId}");

        }
    }
}