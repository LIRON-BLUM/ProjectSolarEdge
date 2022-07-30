using Microsoft.AspNetCore.Components;
using ProjectSolarEdge.Client.Services.GameApp;
using ProjectSolarEdge.Client.Services.Games;
using ProjectSolarEdge.Client.Services.Users;
using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Client.Pages.GamePages
{
    public partial class WheelOfFortune
    {

        public Game GamePlaying { get; set; }

        public UsersTable Player { get; set; }

        public GameScore WheelScoreToInsert { get; set; }


        [Parameter]
        public string GameId { get; set; }

        [Parameter]
        public string UserId { get; set; }

        [Parameter]
        public string WheelScore { get; set; }

        [Inject]
        public IGamesDataService GameDataService { get; set; }

        [Inject]        
        public IGameAppService GameAppDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }



        protected override async Task OnInitializedAsync()
        {
            GamePlaying = await GameDataService.GetGameByIdAsync(int.Parse(GameId));


            Player = await GameAppDataService.GetPlayerByID(int.Parse(UserId));
            //player = new UsersTable()
            //{
            //    ID = 8,
            //    UserFirstName = "Limor",
            //    UserLastName = "Avrahami",
            //    UserName = "LimorAvrahami",
            //};

            // for liron - we need to insert this in the GameScore table




        }

        protected async Task goToNext()
        {
            string check = WheelScore;

            WheelScoreToInsert = new GameScore()
            {
                UserID = Player.ID,
                GameID = GamePlaying.ID,
                GameElement = 1,
                ElementScore = Convert.ToUInt16(WheelScore),

            };

            await GameAppDataService.AddScoreElement(WheelScoreToInsert);

            NavigationManager.NavigateTo($"Gambeling/{GameId}/{UserId}");


        }
    }
}
