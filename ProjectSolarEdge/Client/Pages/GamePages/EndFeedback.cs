using Microsoft.AspNetCore.Components;
using ProjectSolarEdge.Client.Services.Games;
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

        public UsersTable player { get; set; }

        public IEnumerable<GameScore> endFeedback { get; set; }
       
        public Question answerdQuestions { get; set; }

        [Inject]
        public IGamesDataService GameDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async Task backToEnd()
        {
            int gameId = GamePlaying.ID;
            int playerId = player.ID;

            NavigationManager.NavigateTo($"/End/{gameId}/{playerId}");
        }

        //  Liron - delete this after querise
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

            endFeedback = new List<GameScore>()
            {
                new GameScore()
                {
                    QuestionID = 1,
                    IsRight = true
                }
                // new GameScore()
                //{
                //    QuestionID = 2,
                //    IsRight = false
                //},
                //  new GameScore()
                //{
                //    QuestionID = 3,
                //    IsRight = true
                //}

            };

            answerdQuestions = new Question()
            {
             
                ID = 1,
                QuestionBody = "What is your favorite color?",
                Feedback = "My favorit color is purple",
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

            };
                     

        }
    }
}
