using Microsoft.AspNetCore.Components;
using ProjectSolarEdge.Client.Services.Games;
using ProjectSolarEdge.Client.Services.Questions;
using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Client.Pages.GamePages
{
    public partial class LogIn
    {
        public Game GameToPlay { get; set; }

        public UsersTable User { get; set; }

        public string UserEmail { get; set; }
        public string GameCode { get; set; }

        [Inject]
        public IGamesDataService GameDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async Task StartTheGame()
        {
            // liron - check if the user and game exist, if the user dosent exist creat new user 
            string UserEmailTocheck = UserEmail;

            //GameToPlay = await GameDataService.GetGameByIdAsync(int.Parse(GameCode));

            string gameId = GameToPlay.ID.ToString();
            string userId = User.ID.ToString();

            
            NavigationManager.NavigateTo($"/OpeningPage/{gameId}/{userId}");


        }

        protected override async Task OnInitializedAsync()
        {

            User = new UsersTable()
            {
                ID = 8,
                UserFirstName = "Limor",
                UserLastName = "Avrahami",
                UserName = "LimorAvrahami",
            };

            GameToPlay = new Game()
            {
                ID = 3,
            };
        }

    }
}
