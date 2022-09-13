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

        public string noQuestionIMG { get; set; }

        protected override async Task OnInitializedAsync()
        {        
             noQuestionIMG = "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";

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


            currentQuestionNum = GamePlaying.Questions.Count() - availleblQuestions.Count() + 1;


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

            NavigationManager.NavigateTo($"./GetNextStep/{GameId}/{UserId}");

        }
        protected async Task SkipAnawer()
        {
            NavigationManager.NavigateTo($"./GetNextStep/{GameId}/{UserId}");

        }
    }
}