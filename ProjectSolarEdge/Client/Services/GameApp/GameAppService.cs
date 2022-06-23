namespace ProjectSolarEdge.Client.Services.GameApp
{
    public class GameAppService : IGameAppService
    {
        private readonly HttpClient _httpClient;

        public GameAppService(HttpClient client)
        {
            this._httpClient = client;
        }


    }
}
