using Microsoft.AspNetCore.Components;
using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Client.Pages.GamePages
{
    public partial class WheelOfFortune
    {

        public Game GamePlaying { get; set; }

        public UsersTable player { get; set; }


        [Parameter]
        public string GameId { get; set; }

        [Parameter]
        public string UserId { get; set; }

        [Parameter]
        public string WheelScore { get; set; }

        protected async Task goToNext()
        {
            string check = WheelScore;
        }
        protected override async Task OnInitializedAsync()
        {
            string check = GameId;
            string check2 = UserId;

        }
    }
}
