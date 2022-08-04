using Microsoft.AspNetCore.Components;
using ProjectSolarEdge.Client.Services.GameApp;
using ProjectSolarEdge.Client.Services.Games;
using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Client.Pages.GamePages
{
    public partial class End
    {
        [Parameter]
        public string GameId { get; set; }

        [Parameter]
        public string UserId { get; set; }

        public Game GamePlaying { get; set; }

        public UsersTable Player { get; set; }
        public IEnumerable<UserGameScore> TopPlayers { get; set; }
        public IEnumerable<UserGameScore> AllPlayers { get; set; }

        [Inject]
        public IGamesDataService GameDataService { get; set; }

        [Inject]
        public IGameAppService GameAppDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        
             protected async Task GoToFeedback()
        {

            NavigationManager.NavigateTo($"/endFeedback/{GameId}/{UserId}");
        }

        //  Liron - delete this after querise
        protected override async Task OnInitializedAsync()
        {
           

            int.TryParse(GameId, out var GId);

            int.TryParse(UserId, out var UId);

            GamePlaying = await GameDataService.GetGameByIdAsync(GId);


            Player = await GameAppDataService.GetPlayerByID(UId);

            AllPlayers = await GameAppDataService.GetGameUsersScore(GId);

            TopPlayers = AllPlayers.OrderByDescending(e => e.UserScore).Take(3);

            int allUsersCount = AllPlayers.Count();

            AllPlayers = AllPlayers.OrderByDescending(e => e.UserScore).Take(allUsersCount);

         


        }

    }
}
