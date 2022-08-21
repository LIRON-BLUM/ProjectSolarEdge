using MudBlazor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProjectSolarEdge.Client.Services.Users;
using ProjectSolarEdge.Shared.Entities;
using ProjectSolarEdge.Client.Shared;
using Blazored.LocalStorage;

namespace ProjectSolarEdge.Client.Pages
{
    public partial class EditorOpening
    {

        [Parameter]
        public string EditorID { get; set; }

        [CascadingParameter]
        public MainLayout MainLayout { get; set; }

        [CascadingParameter]
        public NavMenu NavLayout { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        [Inject]
        public Blazored.LocalStorage.ISyncLocalStorageService LocalService { get; set; }

        string EditorIDSessiom = "";

        protected override async Task OnInitializedAsync()
        {
            EditorIDSessiom = LocalService.GetItem<string>("SessionValue");
            //EditorIDSessiom = LocalService.GetItem<string>("EditorID");

    
        }

        protected async Task NavQuestion()
        {
            NavigationManager.NavigateTo("./Questions");
        }

        protected async Task NavGames()
        {
            NavigationManager.NavigateTo("./Games");
        }

        protected async Task NavUsers()
        {
            NavigationManager.NavigateTo("./EditQuestion");
        }

    }
}
