﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProjectSolarEdge.Client.Services.Users;
using ProjectSolarEdge.Client.Shared;
using ProjectSolarEdge.Shared.Entities;
using Blazored.LocalStorage;

namespace ProjectSolarEdge.Client.Pages
{
    public partial class EditorLogIn
    {

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        public string EditorID { get; set; }

        public UsersTable UserData { get; set; }

        public string UserEmail { get; set; }

        public string UserPassword { get; set; }

        public UsersTable IdFromUserName { get; set; }

        public UsersTable IdFromPassword { get; set; }

        [CascadingParameter]
        public MainLayout MainLayout { get; set; }

        [CascadingParameter]
        public NavMenu NavLayout { get; set; }

        InputType PasswordInput = InputType.Password;
        bool isShow;
        string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

        [Inject]
        public IUsersDataService UserDataService { get; set; }

        [Inject]
        public Blazored.LocalStorage.ISyncLocalStorageService LocalService { get; set; }

        protected override async Task OnInitializedAsync()
        {

        }
        protected async Task EnterEditor()
        {

            IdFromUserName = await UserDataService.GetUserIdByUserName(UserEmail);
            IdFromPassword = await UserDataService.GetUserIdByUserPassword(UserPassword);

            if (IdFromUserName.ID == IdFromPassword.ID)
            {
                if (IdFromUserName.UserType != UserType.Learner)
                {
                    EditorID = (IdFromUserName.ID).ToString();
                    

                    LocalService.SetItem("EditorID", EditorID);

                    string EditorIDNew = LocalService.GetItem<string>("EditorID");

                    NavigationManager.NavigateTo($"/EditorOpening/{EditorID}");
                }

            }
            else
            {
                EditorID = (IdFromUserName.ID).ToString();
                NavigationManager.NavigateTo($"/EditorOpening/{EditorID}");
            }



            //(QuestionsCRUD.Subjects.Select(s => s.SubjectName)
        }





    }
}
