﻿using ProjectSolarEdge.Shared.Entities;

namespace ProjectSolarEdge.Client.Services.Games
{
    public interface IGamesDataService
    {
    
        Task<IEnumerable<Game>> GetGames();
        Task<IEnumerable<Game>> GetAllGames();
        
    }
}
