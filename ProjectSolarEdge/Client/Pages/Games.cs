using Microsoft.AspNetCore.Components;
using ProjectSolarEdge.Client.Services.Games;
using ProjectSolarEdge.Shared.Entities;
using static System.Net.WebRequestMethods;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
//using MudBlazor.Examples.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using ProjectSolarEdge.Client.Services;

namespace ProjectSolarEdge.Client.Pages
{
    public partial class Games : ComponentBase, IDisposable
    {



      public IEnumerable<Game> GamesData { get; set; }


        [Inject]
        public IGamesDataService GamesDataService { get; set; }



        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            GamesData = await GamesDataService.GetAllGames();
            //  GamesData = await httpClient.GetFromJsonAsync<List<Element>>("webapi/periodictable");

        }

        protected async Task NewGame()
        {

            NavigationManager.NavigateTo("/EditGame");

        }





        bool fixed_header = true;
        bool fixed_footer = false;

        //private string searchString = "";
        private string searchString = "";
        private int totalItems;
        private IEnumerable<Game> pagedData;
        private MudTable<Game> table;
        public string SubName = "";

        //private bool FilterFunc(Game Game)

        protected async Task<TableData<Game>> ServerReload(TableState state)
        {

            IEnumerable<Game> data = await GamesDataService.GetAllGames();
            //await Task.Delay(300);
            data = data.Where(Game =>

            {
                if (string.IsNullOrWhiteSpace(searchString))
                    return true;
                if (Game.GameName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
               
                if ($"{Game.GameTimeLimit} {Game.UpdateDate}".Contains(searchString))
                    return true;
                return false;
            }).ToArray();
            totalItems = data.Count();
            //switch (state.SortLabel)
            //{
            //    case "ID_field":
            //        data = data.OrderByDirection(state.SortDirection, o => o.ID);
            //        break;
            //    case "Type_field":
            //        data = data.OrderByDirection(state.SortDirection, o => o.Type);
            //        break;
            //    case "Difficulty_field":
            //        data = data.OrderByDirection(state.SortDirection, o => o.Difficulty);
            //        break;
            //    case "Creator_field":
            //        data = data.OrderByDirection(state.SortDirection, o => o.Creator);
            //        break;
            //    case "Creation_Date_field":
            //        data = data.OrderByDirection(state.SortDirection, o => o.CreationDate);
            //        break;
            //}

            pagedData = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();
            return new TableData<Game>() { TotalItems = totalItems, Items = pagedData };

        }

        private async Task OnSearch(string text)
        {
            searchString = text;
            table.ReloadServerData();
        }


        public Game GameToDelete { get; set; } = new Game();
        protected async Task GameToDeleteID(int id)
        {
            //GameToDelete = await GameDataService.GetGameByIdAsync(id);

            //DeleteGame();

        }

        protected async Task DeleteQuestion()
        {

            //await GameToDelete.DeleteGame(GameToDelete);

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
