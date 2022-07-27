using Microsoft.AspNetCore.Components;
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

        public UsersTable player { get; set; }

        public Question currentQuestion { get; set; }
        
        public GameQuestionsConnection questionScore { get; set; }
        public string chosenanswer { get; set; }

        public GameScore questionScoreToInsert { get; set; }
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IQuestionsDataService QuestionDataService { get; set; }

        

        protected async Task saveAnawer()
        {
            //  Liron check if the is a row for this question in gameScore table if ther is Update the game score table with this if not insert
            //is the Query supposed to be - update Game score table?
           
            questionScoreToInsert = new GameScore()
            {
                UserID = player.ID,
                GameID = GamePlaying.ID,
                QuestionID = currentQuestion.ID,
                IsRight= Convert.ToBoolean(chosenanswer),
                ElementScore = questionScore.Score,
                IsAnswered= true
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
            
            
            currentQuestion = await QuestionDataService.GetQuestionByIdAsync(int.Parse(QuestionId));
            
            
            // Get question by id from question service

            //currentQuestion = new Question()
            //{
            //    QuestionBody = "What is your favorite color?",
            //    Difficulty = QuestionDifficulty.Medium,
            //    Answers = new List<QuestionAnswer>()
            //    {
            //        new QuestionAnswer() {
            //            AnswerBody= "Red",
            //            IsRight = false
            //        },
            //        new QuestionAnswer() {
            //            AnswerBody= "Blue",
            //            IsRight = false
            //        },
            //        new QuestionAnswer() {
            //            AnswerBody= "Purple",
            //            IsRight = true
            //        },
            //        new QuestionAnswer() {
            //            AnswerBody= "Pink",
            //            IsRight = false
            //        }
                    

            //    }
        
            //};
        }

    }
}
