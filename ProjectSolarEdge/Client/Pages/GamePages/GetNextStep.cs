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

        [Parameter]
        public string cameFromGambling { get; set; }

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


            GamePlaying = await GameDataService.GetGameByIdAsync(gameId);
            string gameGamification = GamePlaying.IsGamified.ToString();

            // we need to get all the question didacting the questions the user had all ready answerd.

            AvailleblQuestions = await GameAppDataService.AvailableQuestions(gameId, userId);



            if (GamePlaying.IsGamified == 1 && cameFromGambling != "true")
            {
                NavigationManager.NavigateTo($"WheelOfFortune/{gameId}/{userId}");


                // First check if gamification is needed
                // Check how many times gamification is required 
                int numberOftimesWheelPresented = usersRecords.Where(r => r.GameElement == 1).Count();

                if (GamePlaying.WheelIteration > numberOftimesWheelPresented)
                {
                    // Here we need to redirect the user to the wheel...
                    // we need to do a random to see what question get gamification
                    NavigationManager.NavigateTo($"WheelOfFortune/{GameId}/{UserId}");
                }
            }


            Question SelectedQuestion = getRandomQuestion();

            //NavigationManager.NavigateTo($"MultipelQuestion/{gameId}/{userId}/{SelectedQuestion.ID}");




            switch (SelectedQuestion.Type)
            {
                case QuestionType.MultipleChoice:
                    {
                        NavigationManager.NavigateTo($"MultipelQuestion/{GameId}/{UserId}/{SelectedQuestion.ID}");
                        break;
                    }
                default:
                case QuestionType.TrueFalse:
                    {
                        NavigationManager.NavigateTo($"YesNoQuestion/{GameId}/{UserId}/{SelectedQuestion.ID}");
                        break;
                    }
                case QuestionType.Order:
                    {
                        NavigationManager.NavigateTo($"OrderQuestion/{GameId}/{UserId}/{SelectedQuestion.ID}");
                        break;
                    }
                    break;
            }
        }

        private Question getRandomQuestion()
        {
            var random = new Random();
            int position = random.Next(AvailleblQuestions.Count());
            return AvailleblQuestions.ToList()[position];
        }
    }
}
