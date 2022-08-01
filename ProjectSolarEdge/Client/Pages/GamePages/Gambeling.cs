using Microsoft.AspNetCore.Components;
using ProjectSolarEdge.Client.Services.GameApp;
using ProjectSolarEdge.Client.Services.Games;
using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Client.Pages.GamePages
{
    public partial class Gambeling
    {
        public Game GamePlaying { get; set; }

        public UsersTable player { get; set; }

        public GameScore GambelingScoreToInsert { get; set; }


        [Parameter]
        public string GameId { get; set; }

        [Parameter]
        public string UserId { get; set; }

        [Parameter]
        public string cameFromGambling { get; set; }

        [Parameter]
        public string GambelScore { get; set; }

        [Inject]
        public IGamesDataService GameDataService { get; set; }

        [Inject]
        public IGameAppService GameAppDataService { get; set; }


        [Inject]
        public NavigationManager NavigationManager { get; set; }


        protected override async Task OnInitializedAsync()
        {
            //  liron - we need to insert this in the GameScore table
            cameFromGambling = "true";

        }


        protected async Task GoToNext()
        {

            GambelingScoreToInsert = new GameScore()
            {
                UserID = int.Parse(UserId),
                GameID = int.Parse(GameId),
                GameElement = 2,
                ElementScore = Convert.ToUInt16(GambelScore),
            };

            string check = GambelScore;

            await GameAppDataService.AddScoreElement(GambelingScoreToInsert);

            NavigationManager.NavigateTo($"GetNextStep/{GameId}/{UserId}/{cameFromGambling}");
        }

    }
}
