using Microsoft.AspNetCore.Components;
using ProjectSolarEdge.Client.Services.Games;
using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Client.Pages.GamePages
{
    public partial class WheelOfFortiune
    {
        [Parameter]
        public string Id { get; set; }

        public Game GamePlaying { get; set; } = new Game();

        public IEnumerable<Game> GameData { get; set; }

        [Inject]
        public IGamesDataService GameDataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            GamePlaying = await GameDataService.GetGameByIdAsync(int.Parse(Id));
        }
    }

   
}
