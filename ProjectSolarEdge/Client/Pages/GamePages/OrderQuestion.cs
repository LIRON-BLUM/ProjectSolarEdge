using Microsoft.AspNetCore.Components;
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
        
        // check
        public string chosenanswer { get; set; }

        public GameScore questionScoreToInsert { get; set; }

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
                IsRight = Convert.ToBoolean(chosenanswer),
                ElementScore = questionScore.Score,
                IsAnswered = true
            };

            int gameId = GamePlaying.ID;
            NavigationManager.NavigateTo($"/GetNextStep/{gameId}");
        }

        protected async Task SkipAnawer()
        {
            int gameId = GamePlaying.ID;
            NavigationManager.NavigateTo($"/GetNextStep/{gameId}");
        }
        protected override async Task OnInitializedAsync()
        {
            // Get question by id from question service

            currentQuestion = new Question()
            {
                QuestionBody = "Sort the list of names so the oldest person is on top",
                Difficulty = QuestionDifficulty.Medium,
                Answers = new List<QuestionAnswer>()
                {
                    new QuestionAnswer() {
                        AnswerBody= "Yotam Avrahami",
                        QuestionID= 1
                        //located = 1
                    },
                    new QuestionAnswer() {
                        AnswerBody= "Limor Avrahami",
                         QuestionID= 2

                       //located = 2
                    },
                    new QuestionAnswer() {
                        AnswerBody= "Roi Ben-Zvi",
                          QuestionID= 3

                       //located = 3
                    },
                    new QuestionAnswer() {
                        AnswerBody= "Liron Blum",
                          QuestionID= 4

                       //located = 4
                    }

                }

            };
        }

    }
}

