using Microsoft.AspNetCore.Components;
using ProjectSolarEdge.Client.Services.Games;
using ProjectSolarEdge.Client.Services.Questions;
using ProjectSolarEdge.Client.Services.Users;
using ProjectSolarEdge.Shared.Entities;
using MudBlazor;

namespace ProjectSolarEdge.Client.Pages.GamePages
{
    public partial class LogIn
    {

        MudForm form;

        public string GameId { get; set; }

       
        public string UserId { get; set; }

        public Game GameToPlay { get; set; }

        public UsersTable User { get; set; }

        public string UserEmail { get; set; }
        public string GameCode { get; set; }

      

        [Inject]
        public IGamesDataService GameDataService { get; set; }

        [Inject]
        public IUsersDataService UserDataService { get; set; }


        [Inject]
        public NavigationManager NavigationManager { get; set; }



        //protected override async Task OnInitializedAsync()
        //{

        //    //Select UserID where UserName = UserEmail


        //    //  User = new UsersTable()
        //    //{
        //    //    ID = 8,
        //    //    UserFirstName = "Limor",
        //    //    UserLastName = "Avrahami",
        //    //    UserName = "LimorAvrahami",
        //    //};

        //    //GameToPlay = new Game()
        //    //{
        //    //    ID = 3,
        //    //};
        //}


        protected async Task StartTheGame()
        {

            //string UserEmailTocheck = UserEmail;

            //GameToPlay = await GameDataService.GetGameByIdAsync(int.Parse(GameCode));
            User = await UserDataService.GetUserIdByUserName(UserEmail);

            // liron - check if the user and game exist, if the user dosent exist create new user 


            //GameToPlay = await GameDataService.GetGameByIdAsync(int.Parse(GameCode));


       

            int gameID = int.Parse(GameCode);
            int userID = User.ID;

            NavigationManager.NavigateTo($"/OpeningPage/{gameID}/{userID}");


        }

        //protected async Task NavigateToGame(int GameId, int UserId)
        //{
        //    NavigationManager.NavigateTo($"/OpeningPage/{GameId}/{UserId}");
        //    //NavigationManager.NavigateTo("/OpeningPage/{GameId}/{UserId}");
        //}

    }
}
