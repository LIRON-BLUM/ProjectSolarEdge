﻿using ProjectSolarEdge.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSolarEdge.Shared.Services.GameApp
{
    public interface IGameAppRepository
    {
        IEnumerable<UsersGameRecord> GetUsersGameRecordByGameId(int ID);

        IEnumerable<UsersGameRecord> GetAllUsersGameRecord();

        IEnumerable<UsersGameRecord> AvailableQuestions(int gameID, int userID);

        UsersTable GetPlayerByID(int userID);

    }
}
