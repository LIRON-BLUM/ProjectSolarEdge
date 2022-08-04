

using Microsoft.AspNetCore.Components;
using ProjectSolarEdge.Client.Services.GameApp;
using ProjectSolarEdge.Client.Services.Games;
using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Client.Pages.GamePages
{ 
    public partial class GameOpeningPage
    {

    [Parameter]
    public string GameId { get; set; }

    [Parameter]
    public string UserId { get; set; }

    public Game GamePlaying { get; set; }

    public UsersTable Player { get; set; }
    public IEnumerable<UserGameScore> TopPlayers { get; set; }

    [Inject]
    public IGamesDataService GameDataService { get; set; }

    [Inject]
    public IGameAppService GameAppDataService { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }


        protected override async Task OnInitializedAsync()
        {
            GamePlaying = await GameDataService.GetGameByIdAsync(int.Parse(GameId));
            TopPlayers = await GameAppDataService.GetGameUsersScore(int.Parse(GameId));

            Player = new UsersTable()
            {
                ID = 1,
                UserFirstName = "Limor",
                UserLastName = "Avrahami",
                UserName = "LimorAvrahami",
            };


            //  Liron - delete this after querise

            //TopPlayers = new List<UsersGameRecord>()
            //{
            //    new UsersGameRecord()
            //    {
            //        UserFirstName = "Adi",
            //        ID =1,
            //        UserLastName ="Silagy",
            //        UserName ="AdiSilagi",
            //        TotalScore=3000
            //    },

            //    new UsersGameRecord()
            //    {
            //        UserFirstName = "Moti",
            //        ID =2,
            //        UserLastName ="Elnekave",
            //        UserName ="MotiElnekave",
            //        TotalScore=2000
            //    },
            //      new UsersGameRecord()
            //    {
            //        UserFirstName = "Liron",
            //        ID =3,
            //        UserLastName ="Blum",
            //        UserName ="LironBlum",
            //        TotalScore=2500
            //    }
            //};

            TopPlayers = TopPlayers.OrderByDescending(e => e.UserScore).Take(3);
        }


        protected async Task STARTGAME()
        {
            int gameId = GamePlaying.ID;
            NavigationManager.NavigateTo($"/GetNextStep/{gameId}");
        }

    }
}
