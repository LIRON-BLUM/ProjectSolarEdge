using Microsoft.AspNetCore.Components;
using ProjectSolarEdge.Client.Services.GameApp;
using ProjectSolarEdge.Client.Services.Games;
using ProjectSolarEdge.Client.Services.Questions;
using ProjectSolarEdge.Client.Services.Users;
using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Client.Pages.GamePages
{
    public partial class EndFeedback
    {
        [Parameter]
        public string GameId { get; set; }

        [Parameter]
        public string UserId { get; set; }

        public Game GamePlaying { get; set; }

        public UsersTable Player { get; set; }

        public IEnumerable<GameScore> UserQuestionsAnswers { get; set; }

        public GameScore UserQuestionsIsRight { get; set; }

        public Question answerdQuestions { get; set; }

        public IEnumerable<Question> AnswerdQuestions { get; set; }

        public List<Question> selectedQuestionToShow = new List<Question>();

        [Inject]
        public IGamesDataService GameDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IUsersDataService UserDataService { get; set; }

        [Inject]
        public IGameAppService GameAppDataService { get; set; }

        [Inject]
        public IQuestionsDataService QuestionDataService { get; set; }

        public bool QisRight { get; set; }


        protected async Task backToEnd()
        {
            
            NavigationManager.NavigateTo($"./End/{GameId}/{UserId}");
        }

        //  Liron - delete this after querise
        protected override async Task OnInitializedAsync()
        {
            int.TryParse(GameId, out var gameID);
            int.TryParse(UserId, out var userID);

            GamePlaying = await GameDataService.GetGameByIdAsync(gameID);
            Player = await GameAppDataService.GetPlayerByID(userID);


            UserQuestionsAnswers =  await GameAppDataService.GetAllUserGameScore(gameID, userID);

            //SELECT * FROM GameScore WHERE GameID=@GameID AND UserID=@UserID
            //UserQuestionsAnswers = new List<GameScore>()
            //{
            //    new GameScore()
            //    {
            //        QuestionID = 1,
            //        IsRight = true
            //    }


            //};

            //List<Question> selectedQuestionToShow = new List<Question>();

            foreach (var question in UserQuestionsAnswers)
            {
              answerdQuestions =  await QuestionDataService.GetQuestionByIdAsync(question.QuestionID);

                selectedQuestionToShow.Add(answerdQuestions);
            }

            UserQuestionsAnswers = UserQuestionsAnswers.OrderByDescending(e => e.QuestionID);
            selectedQuestionToShow = (List<Question>)selectedQuestionToShow.OrderByDescending(q => q.ID);


            foreach(var question in selectedQuestionToShow)
            {

                  
                    foreach(var ans in question.Answers)
                {
                   
                }

            }


                //answerdQuestions = new Question()
                //{

                //    ID = 1,
                //    QuestionBody = "What is your favorite color?",
                //    Feedback = "My favorit color is purple",
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



                //new Question()
                //{
                //ID = 2,
                //QuestionBody = "What is your name?",
                //Feedback = "My name is Limor",
                //Answers = new List<QuestionAnswer>()
                //{
                //    new QuestionAnswer() {
                //        AnswerBody= "Limor",
                //    },
                //    new QuestionAnswer() {
                //        AnswerBody= "Liron",
                //    },
                //    new QuestionAnswer() {
                //        AnswerBody= "Lirom",
                //    },
                //    new QuestionAnswer() {
                //        AnswerBody= "Limon",
                //    }
                //}

                //},
                //new Question()
                //{
                //ID = 3,
                //QuestionBody = "Wher do yo live?",
                //Feedback = "I live in New York",
                //Answers = new List<QuestionAnswer>()
                //{
                //    new QuestionAnswer() {
                //        AnswerBody= "Arad",
                //    },
                //    new QuestionAnswer() {
                //        AnswerBody= "Hedera",
                //    },
                //    new QuestionAnswer() {
                //        AnswerBody= "Petah Tikva",
                //    },
                //    new QuestionAnswer() {
                //        AnswerBody= "New York",
                //    }
                //}

                //}

            }



        }
}
