using Microsoft.AspNetCore.Components;
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
        public string GambelScore { get; set; }

        [Inject]
        public IGamesDataService GameDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async Task goToNext()
        {
            string check = GambelScore;

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

            //  liron - we need to insert this in the GameScore table
            GambelingScoreToInsert = new GameScore()
            {
                UserID = player.ID,
                GameID = GamePlaying.ID,
                GameElement = 2,
                ElementScore = Convert.ToUInt16(GambelScore),
            };

        }

    }
}
