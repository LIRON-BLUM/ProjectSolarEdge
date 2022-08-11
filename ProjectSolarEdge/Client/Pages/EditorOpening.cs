using MudBlazor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProjectSolarEdge.Client.Services.Users;
using ProjectSolarEdge.Shared.Entities;
using ProjectSolarEdge.Client.Shared;

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




    }
}
