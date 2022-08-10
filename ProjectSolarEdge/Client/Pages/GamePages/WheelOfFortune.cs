using Microsoft.AspNetCore.Components;
using ProjectSolarEdge.Client.Services.GameApp;
using ProjectSolarEdge.Client.Services.Games;
using ProjectSolarEdge.Client.Services.Users;
using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Client.Pages.GamePages
{
    public partial class WheelOfFortune : ComponentBase, IDisposable
    {

        public Game GamePlaying { get; set; }

        public UsersTable Player { get; set; }

        public GameScore WheelScoreToInsert { get; set; }


        [Parameter]
        public string GameId { get; set; }

        [Parameter]
        public string UserId { get; set; }

        [Parameter]
        public string WheelScore { get; set; }

        [Inject]
        public IGamesDataService GameDataService { get; set; }

        [Inject]        
        public IGameAppService GameAppDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }



        protected override async Task OnInitializedAsync()
        {



        }

        protected async Task GoToNext()
        {
            string check = WheelScore;

            WheelScoreToInsert = new GameScore()
            {
                UserID = int.Parse(UserId),
                GameID = int.Parse(GameId),
                GameElement = 1,
                GamblingScore=0,
                ElementScore = Convert.ToUInt16(WheelScore),

            };

            await GameAppDataService.AddScoreElement(WheelScoreToInsert);

            NavigationManager.NavigateTo($"Gambeling/{GameId}/{UserId}");


        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
