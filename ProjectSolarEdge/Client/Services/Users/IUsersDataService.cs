﻿using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Client.Services.Users
{
    public interface IUsersDataService
    {
        Task<UsersTable> GetUserIdByUserName(string UserName);
    }
}