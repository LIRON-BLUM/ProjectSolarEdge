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

            //Player = new UsersTable()
            //{
            //    ID = 8,
            //    UserFirstName = "Limor",
            //    UserLastName = "Avrahami",
            //    UserName = "LimorAvrahami",
            //};

            //TopPlayers = await GameAppDataService.GetUsersGameRecord();

            //PlayrsByGameID = await GameAppDataService.GetUsersGameRecordById(int.Parse(GameId));

            TopPlayers = await GameAppDataService.GetUsersGameRecordByGameId(int.Parse(GameId));

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

            //Question q = QuestionsData.Where(q => q.ID == item.ID).SingleOrDefault();
            //selectedQuestionToUpdate.Add(q);

            //UsersGameRecord p = TopPlayers.Where(p => p.GameID == GId).SingleOrDefault();

            //TopThreePlayers = new List<UsersGameRecord>()
            //{
            //    new UsersGameRecord()
            //    {
            //    ID = p.ID,
            //    UserFirstName = p.UserFirstName,
            //    UserLastName = p.UserLastName,
            //    UserName = p.UserName,
            //    TotalScore = p.TotalScore
            //    }
            //};




            TopPlayers = TopPlayers.OrderByDescending(e => e.TotalScore).Take(3);
        }


        protected async Task STARTGAME()
        {
            int gameId = GamePlaying.ID;
            NavigationManager.NavigateTo($"/GetNextStep/{gameId}");
        }

        public void Dispose()
        { 
           throw new NotImplementedException();
        }

    }
}
