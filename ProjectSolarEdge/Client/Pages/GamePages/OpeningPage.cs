using Microsoft.AspNetCore.Components;
using ProjectSolarEdge.Client.Services.GameApp;
using ProjectSolarEdge.Client.Services.Games;
using ProjectSolarEdge.Shared.Entities;




namespace ProjectSolarEdge.Client.Pages.GamePages
{
    public partial class OpeningPage : ComponentBase, IDisposable
    {

    
        [Parameter]
        public string GameId { get; set; }

        [Parameter]
        public string UserId { get; set; }

        [Parameter]
        public string cameFromGambling { get; set; }

        public Game GamePlaying { get; set; }

        public UsersTable Player { get; set; }
        public IEnumerable<UsersGameRecord> TopPlayers { get; set; } 

        public UsersGameRecord PlayrsByGameID { get; set; }

        [Inject]
        public IGamesDataService GameDataService { get; set; }

        [Inject]
        public IGameAppService GameAppDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public List<UsersGameRecord> TopThreePlayers { get; private set; }

        protected override async Task OnInitializedAsync()
        {

            int.TryParse(GameId, out var GId);

            GamePlaying = await GameDataService.GetGameByIdAsync(GId);
          

            Player = await GameAppDataService.GetPlayerByID(int.Parse(UserId));

            TopPlayers = await GameAppDataService.GetUsersGameRecordByGameId(int.Parse(GameId));

            TopPlayers = TopPlayers.OrderByDescending(e => e.TotalScore).Take(3);

           

        }


        protected async Task STARTGAME()
        {
            cameFromGambling = "false";
            NavigationManager.NavigateTo($"/GetNextStep/{GameId}/{UserId}");
            //NavigationManager.NavigateTo($"GetNextStep/{GameId}/{UserId}/{cameFromGambling}");
        }

        public void Dispose()
        { 
           throw new NotImplementedException();
        }

    }
}
