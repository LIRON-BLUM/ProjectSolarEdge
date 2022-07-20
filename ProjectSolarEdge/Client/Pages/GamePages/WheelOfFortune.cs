using Microsoft.AspNetCore.Components;
using ProjectSolarEdge.Client.Services.Games;
using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Client.Pages.GamePages
{
    public partial class WheelOfFortune
    {

        public Game GamePlaying { get; set; }

        public UsersTable player { get; set; }

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
        public NavigationManager NavigationManager { get; set; }

        protected async Task goToNext()
        {
            string check = WheelScore;
            int gameId = GamePlaying.ID;
            int playerId = player.ID;
            NavigationManager.NavigateTo($"/Gambeling/{gameId}/{playerId}");

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

            // for liron - we need to insert this in the GameScore table
            WheelScoreToInsert = new GameScore()
            {
                UserID = player.ID,
                GameID = GamePlaying.ID,
                GameElement = 1,
                ElementScore = Convert.ToUInt16(WheelScore),
            };

        }
    }
}
