using Microsoft.AspNetCore.Components;

namespace ProjectSolarEdge.Client.Pages
{
    public partial class EditorLogIn
    {

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async Task EnterEditor()
        {
            NavigationManager.NavigateTo($"/EditorOpening");

        }

    }
}
