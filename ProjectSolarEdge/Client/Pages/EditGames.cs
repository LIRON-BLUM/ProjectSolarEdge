using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ProjectSolarEdge.Client.Services.Questions;
using ProjectSolarEdge.Shared.Entities;
using MudBlazor;
using ProjectSolarEdge.Client.Services.Games;

namespace ProjectSolarEdge.Client.Pages
{
    public partial class EditGames
    {
        [Parameter]
        public string Id { get; set; }

        public Game GameCRUD { get; set; } = new Game();

        public IEnumerable<Game> GameData { get; set; }

        [Inject]
        public IGamesDataService GameDataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            GameCRUD = await GameDataService.GetGameByIdAsync(int.Parse(Id));
        }
    }
}
