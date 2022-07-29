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

        public UsersTable player { get; set; }
        public IEnumerable<UsersGameRecord> TopPlayers { get; set; }
        public IEnumerable<UsersGameRecord> allPlayers { get; set; }

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
            GamePlaying = await GameDataService.GetGameByIdAsync(int.Parse(GameId));

            player = new UsersTable()
            {
                ID = 8,
                UserFirstName = "Limor",
                UserLastName = "Avrahami",
                UserName = "LimorAvrahami",
            };

            TopPlayers = new List<UsersGameRecord>()
            {
                new UsersGameRecord()
                {
                    UserFirstName = "Adi",
                    ID =1,
                    UserLastName ="Silagy",
                    UserName ="AdiSilagi",
                    TotalScore=3000
                },

                new UsersGameRecord()
                {
                    UserFirstName = "Moti",
                    ID =2,
                    UserLastName ="Elnekave",
                    UserName ="MotiElnekave",
                    TotalScore=2000
                },
                  new UsersGameRecord()
                {
                    UserFirstName = "Liron",
                    ID =3,
                    UserLastName ="Blum",
                    UserName ="LironBlum",
                    TotalScore=2500
                },

            };

            TopPlayers = TopPlayers.OrderByDescending(e => e.TotalScore).Take(3);

            //allPlayers = (IEnumerable<UsersGameRecord>)await GameAppDataService.GetUsersGameRecordById(int.Parse(GameId));

            allPlayers = new List<UsersGameRecord>()
            {
                new UsersGameRecord()
                {
                    UserFirstName = "Adi",
                    ID =1,
                    UserLastName ="Silagy",
                    UserName ="AdiSilagi",
                    TotalScore=3000
                },

                new UsersGameRecord()
                {
                    UserFirstName = "Moti",
                    ID =2,
                    UserLastName ="Elnekave",
                    UserName ="MotiElnekave",
                    TotalScore=2000
                },
                  new UsersGameRecord()
                {
                    UserFirstName = "Liron",
                    ID =3,
                    UserLastName ="Blum",
                    UserName ="LironBlum",
                    TotalScore=2500
                },
                     new UsersGameRecord()
                {
                    UserFirstName = "Yotam",
                    ID =4,
                    UserLastName ="Avrahami",
                    UserName ="YotamAvrahami",
                    TotalScore=2400
                },
                        new UsersGameRecord()
                {
                    UserFirstName = "Roi",
                    ID =3,
                    UserLastName ="Ben-Zvi",
                    UserName ="RoiBenZVI",
                    TotalScore=2100
                },
                        new UsersGameRecord()
                {
                    UserFirstName = "Limor",
                    ID = 8,
                    UserLastName = "Avrahami",
                    UserName = "LimorAvrahami",
                    TotalScore=2000
                }
            };
            int allUsersCount = allPlayers.Count();

            allPlayers = allPlayers.OrderByDescending(e => e.TotalScore).Take(allUsersCount);


        }

    }
}
