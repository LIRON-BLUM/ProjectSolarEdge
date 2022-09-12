using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ProjectSolarEdge.Client.Services.Questions;
using ProjectSolarEdge.Shared.Entities;
using MudBlazor;
using System.Linq;
using System.Collections.Generic;
using ProjectSolarEdge.Client.Services.Users;
using ProjectSolarEdge.Client.Shared;
using System.IO;

namespace ProjectSolarEdge.Client.Pages
{
    public partial class EditUser : ComponentBase, IDisposable
    {

        [Parameter]
        public string Id { get; set; }

        [Inject]
        public IUsersDataService UsersDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public UsersTable UserCRUD { get; set; } = new UsersTable();

        public string DefaultValue { get; set; } = "Select User Type";

        public IEnumerable<string> SelectedType { get; set; } = new HashSet<string>();

        InputType PasswordInput = InputType.Password;
        bool isShow;
        string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;


        protected override async Task OnInitializedAsync()
        {
            int.TryParse(Id, out var UId);
          
            if (UId == 0)
            {
                UserCRUD = new UsersTable { UserType = (UserType)1, CreationDate = DateTime.Now, UpdateDate = DateTime.Now };
            }
            else
            {
             UserCRUD = await UsersDataService.GetUsererByID(UId);

            }
            
            
        }


        protected async Task AddAndUpdate()
        {
            int.TryParse(Id, out var UId);

            if (UId == 0)
            {
                int UserID = await UsersDataService.AddUserToDB(UserCRUD);
           
            if (UserID != 0)
                {
                    UserCRUD.ID = UserID;
                }
            } else
            {
                await UsersDataService.UpdateUser(UserCRUD);
            }

            NavigationManager.NavigateTo("./Users");
        }



        protected async Task DeleteUser()
        {

            await UsersDataService.DeleteUser(UserCRUD);


           
            NavigationManager.NavigateTo("./Users");


        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
