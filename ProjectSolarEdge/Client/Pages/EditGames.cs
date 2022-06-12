using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ProjectSolarEdge.Client.Services.Questions;
using ProjectSolarEdge.Shared.Entities;
using MudBlazor;
using ProjectSolarEdge.Client.Services.Games;

namespace ProjectSolarEdge.Client.Pages
{
    public partial class EditGames
    {
        [Parameter]
        public string Id { get; set; }

        public Game GameCRUD { get; set; } = new Game();

        public IEnumerable<Game> GameData { get; set; }

        [Inject]
        public IGamesDataService GameDataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            GameCRUD = await GameDataService.GetGameByIdAsync(int.Parse(Id));
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

            IEnumerable<Game> data = await GameDataService.GetAllGames();
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

    }
}
