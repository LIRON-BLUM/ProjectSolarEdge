using Microsoft.AspNetCore.Components;
using ProjectSolarEdge.Client.Services.Games;
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

        public UsersTable player { get; set; }

        public Question currentQuestion { get; set; }
        
        public GameQuestionsConnection questionScore { get; set; }
        public string chosenanswer { get; set; }

        public GameScore questionScoreToInsert { get; set; }
        public IEnumerable<GameScore>  gameCurrentScore { get; set; }

        int currentScore = 200;

        public IEnumerable<Question> availleblQuestions { get; set; }


        [Inject]
        public IGamesDataService GameDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async Task saveAnawer()
        {
            //  Liron check if the is a row for this question in gameScore table if ther is Update the game score table with this if not insert
           
            questionScoreToInsert = new GameScore()
            {
                UserID = player.ID,
                GameID = GamePlaying.ID,
                QuestionID = currentQuestion.ID,
                IsRight= Convert.ToBoolean(chosenanswer),
                ElementScore = questionScore.Score,
                IsAnswered= true
            };

            NavigationManager.NavigateTo($"GetNextStep/{GameId}/{UserId}");

        }

        protected async Task SkipAnawer()
        {
            NavigationManager.NavigateTo($"GetNextStep/{GameId}/{UserId}");

        }

        protected override async Task OnInitializedAsync()
        {
            GamePlaying = await GameDataService.GetGameByIdAsync(int.Parse(GameId));

            player = new UsersTable()
            {
                ID = 8,
                UserFirstName = "Limor",
                UserLastName = "Avrahami",
                UserName = "LimorAvrahami",
            };


            // Get question by id from question service

            currentQuestion = new Question()
            {
                ID=1,
                QuestionBody = "What is your favorite color?",
                Difficulty = QuestionDifficulty.Medium,
                Answers = new List<QuestionAnswer>()
                {
                    new QuestionAnswer() {
                        AnswerBody= "Red",
                        IsRight = false
                    },
                    new QuestionAnswer() {
                        AnswerBody= "Blue",
                        IsRight = false
                    },
                    new QuestionAnswer() {
                        AnswerBody= "Purple",
                        IsRight = true
                    },
                    new QuestionAnswer() {
                        AnswerBody= "Pink",
                        IsRight = false
                    }

                }

            };

            questionScore = new GameQuestionsConnection()
            {
                Score = 200
             };


            availleblQuestions = new List<Question>()
            {
                new Question()
                {
                    ID = 1,
                    Type= QuestionType.SingleChoice
                },
                  new Question() {
                    ID = 2,
                    Type= QuestionType.TrueFalse
                },


            };

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

        }

    }
}
