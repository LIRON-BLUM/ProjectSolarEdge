using Microsoft.AspNetCore.Components;
using ProjectSolarEdge.Client.Services.GameApp;
using ProjectSolarEdge.Client.Services.Games;
using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Client.Pages.GamePages
{
    public partial class GetNextStep
    {
        [Parameter]
        public string GameId { get; set; }

        [Parameter]
        public string UserId { get; set; }

        public Game GamePlaying { get; set; }

        public UsersTable player { get; set; }

        public IEnumerable<Question> AvailleblQuestions { get; set; }

        public IEnumerable<GameScore> usersRecords { get; set; }

        [Inject]
        public IGamesDataService GameDataService { get; set; }

        [Inject]
        public IGameAppService GameAppDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {

            int.TryParse(GameId, out var gameId);
            int.TryParse(UserId, out var userId);


            GamePlaying = await GameDataService.GetGameByIdAsync(int.Parse(GameId));
            string gameGamification = GamePlaying.IsGamified.ToString();

            // we need to get all the question didacting the questions the user had all ready answerd.

            AvailleblQuestions = await GameAppDataService.AvailableQuestions(gameId, userId);

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

            //    new Question()   {
            //        ID = 3,
            //        Type= QuestionType.MultipleChoice
            //    },

            //    new Question(){ID = 4,
            //        Type= QuestionType.TrueFalse
            //    }

            //};



            if (GamePlaying.IsGamified == 1)
            {
                NavigationManager.NavigateTo($"WheelOfFortune/{GameId}/{UserId}");
               
                
                //// First check if gamification is needed
                //// Check how many times gamification is required 
                //int numberOftimesWheelPresented = usersRecords.Where(r => r.GameElement == 1).Count();

                //if (GamePlaying.WheelIteration > numberOftimesWheelPresented)
                //{
                //    // Here we need to redirect the user to the wheel...
                //    // we need to do a random to see what question get gamification
                //    NavigationManager.NavigateTo($"WheelOfFortune/{GameId}/{UserId}");
                //}
            }
            else
            {

            Question SelectedQuestion = getRandomQuestion();

            NavigationManager.NavigateTo($"MultipelQuestion/{GameId}/{UserId}/{SelectedQuestion.ID}");
            }



            //switch (SelectedQuestion.Type)
            //{
            //    case QuestionType.SingleChoice:
            //        {
            //            NavigationManager.NavigateTo($"MultipelQuestion/{GameId}/{UserId}/{SelectedQuestion.ID}");
            //            break;
            //        }
            //    default:
            //    case QuestionType.TrueFalse:
            //        {
            //            NavigationManager.NavigateTo($"YesNoQuestion/{GameId}/{UserId}/{SelectedQuestion.ID}");
            //            break;
            //        }
            //    case QuestionType.MultipleChoice:
            //        {
            //            NavigationManager.NavigateTo($"OrderQuestion/{GameId}/{UserId}/{SelectedQuestion.ID}");
            //            break;
            //        }
            //        break;
            //}
        }

        private Question getRandomQuestion()
        {
            var random = new Random();
            int position = random.Next(AvailleblQuestions.Count());
            return AvailleblQuestions.ToList()[position];
        }
    }
}
