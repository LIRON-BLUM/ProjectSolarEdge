using Microsoft.AspNetCore.Components;
using ProjectSolarEdge.Client.Services.GameApp;
using ProjectSolarEdge.Client.Services.Games;
using ProjectSolarEdge.Shared.Entities;


namespace ProjectSolarEdge.Client.Pages.GamePages
{
    public partial class OpeningPage
    {
        [Parameter]
        public string GameId { get; set; }

        [Parameter]
        public string UserId { get; set; }

        public Game GamePlaying { get; set; }

        public UsersTable player { get; set; }
        public IEnumerable<UsersGameRecord>  TopPlayers { get; set; }

        [Inject]
        public IGamesDataService GameDataService { get; set; }

        [Inject]
        public IGameAppService GameAppDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async Task STARTGAME()
        {
            int gameId = GamePlaying.ID;
            NavigationManager.NavigateTo($"/GetNextStep/{gameId}" );
        }

        //  Liron - delete this after querise
        protected override async Task OnInitializedAsync()
        {
            GamePlaying = await GameDataService.GetGameByIdAsync(int.Parse(GameId));
            TopPlayers = (IEnumerable<UsersGameRecord>)await GameAppDataService.GetUsersGameRecordById(int.Parse(GameId));

            player = new UsersTable()
            {
                ID = 8,
                UserFirstName = "Limor",
                UserLastName = "Avrahami",
                UserName = "LimorAvrahami",
            };

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

            TopPlayers = TopPlayers.OrderByDescending(e => e.TotalScore).Take(3);
        }

    }
}
