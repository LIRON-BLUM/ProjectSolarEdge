using Microsoft.AspNetCore.Components;
using static System.Net.WebRequestMethods;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
//using MudBlazor.Examples.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using ProjectSolarEdge.Client.Services;
using ProjectSolarEdge.Client.Shared;
using ProjectSolarEdge.Client.Services.Questions;
using ProjectSolarEdge.Shared.Entities;
using ProjectSolarEdge.Client.Services.Users;

namespace ProjectSolarEdge.Client.Pages
{
    public partial class Users : ComponentBase, IDisposable
    {

      
        public IEnumerable<UsersTable> UsersData { get; set; }
        public IEnumerable<UsersTable> UsersDataToDisplay { get; set; }

        [Inject]
        public IUsersDataService UserDataService { get; set; }

        public UsersTable UserToDelete { get; set; } = new UsersTable();

        public UsersTable UserToEdit { get; set; } = new UsersTable();

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        [Inject]
        public Blazored.LocalStorage.ISyncLocalStorageService LocalService { get; set; }

        string EditorIDSessiom = "";


        protected override async Task OnInitializedAsync()
        {
            UsersData = await UserDataService.GetAllUsers();
            UsersDataToDisplay = UsersData;
          

            EditorIDSessiom = LocalService.GetItem<string>("SessionValue");

            string test = EditorIDSessiom;

        }



        protected async Task NavigateToEdit(int Uid)
        {

   
            NavigationManager.NavigateTo($"./EditUser/{Uid}");

        }


        protected async Task NavNewUser()
        {
            NavigationManager.NavigateTo($"./EditUser");
        }

        bool fixed_header = true;
        bool fixed_footer = false;

        //private string searchString = "";
        private string searchString = "";
        private int totalItems;
        private IEnumerable<UsersTable> pagedData;
        private MudTable<UsersTable> table;
        public string SubName = "";

        //private bool FilterFunc(Question question)

        protected async Task<TableData<UsersTable>> ServerReload(TableState state)
        {

            IEnumerable<UsersTable> data = await UserDataService.GetAllUsers();

            data = data.Where(user =>

            {
                if (string.IsNullOrWhiteSpace(searchString))
                    return true;
                if (user.UserFirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (user.UserLastName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if ($"{user.UserType} {user.UpdateDate}".Contains(searchString))
                    return true;
                return false;
            }).ToArray();
            totalItems = data.Count();




            switch (state.SortLabel)
            {
                case "ID_field":
                    data = data.OrderByDirection(state.SortDirection, o => o.ID);
                    break;
                case "FirstName_field":
                    data = data.OrderByDirection(state.SortDirection, o => o.UserFirstName);
                    break;
                case "LastName_field":
                    data = data.OrderByDirection(state.SortDirection, o => o.UserLastName);
                    break;
                case "Creation_Date_field":
                    data = data.OrderByDirection(state.SortDirection, o => o.CreationDate);
                    break;
            
            }

            pagedData = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();
            return new TableData<UsersTable>() { TotalItems = totalItems, Items = pagedData };

        }

        private async Task OnSearch(string text)
        {
            UsersDataToDisplay = UsersData.Where(q => q.UserFirstName.Contains(text) || q.UserLastName.ToLower().Contains(text.ToLower()));
            //searchString = text;
            //table.ReloadServerData();
        }



        protected async Task DeleteUser(int id)
        {
            UserToDelete = await UserDataService.GetUsererByID(id);
            await UserDataService.DeleteUser(UserToDelete);
            UsersData = await UserDataService.GetAllUsers();
            UsersDataToDisplay = UsersData;

            NavigationManager.NavigateTo("./Users");




        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
