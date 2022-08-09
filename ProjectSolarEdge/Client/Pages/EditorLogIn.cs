using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProjectSolarEdge.Client.Services.Users;
using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Client.Pages
{
    public partial class EditorLogIn
    {

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public UsersTable UserData { get; set; }

        public string UserEmail { get; set; }

        public string UserPassword { get; set; }

        public UsersTable IdFromUserName { get; set; }

        public UsersTable IdFromPassword { get; set; }

        InputType PasswordInput = InputType.Password;
        bool isShow;
        string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

        [Inject]
        public IUsersDataService UserDataService { get; set; }

        protected override async Task OnInitializedAsync()
        {

        }
            protected async Task EnterEditor()
        {

            IdFromUserName = await UserDataService.GetUserIdByUserName(UserEmail);
            IdFromPassword =  await UserDataService.GetUserIdByUserPassword(UserPassword);

            if (IdFromUserName.ID == IdFromPassword.ID)
            {
                NavigationManager.NavigateTo($"/EditorOpening");
            }





            //(QuestionsCRUD.Subjects.Select(s => s.SubjectName)
        }



      

    }
}
