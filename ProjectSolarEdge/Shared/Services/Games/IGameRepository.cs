﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Shared.Services.Games
{
    public interface IGameRepository
    {

        IEnumerable<Game> GetGames();
    }
}
