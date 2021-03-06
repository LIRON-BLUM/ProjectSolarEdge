using Microsoft.AspNetCore.Components;
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
        public NavigationManager NavigationManager { get; set; }

        protected async Task goToNext()
        {
            string check = GambelScore;

        }
        protected override async Task OnInitializedAsync()
        {
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
