﻿using ProjectSolarEdge.Shared.Data;
using ProjectSolarEdge.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSolarEdge.Shared.Services.Games
{
    public class GameRepository : DbRepository, IGameRepository
    {
        public GameRepository(string connectionString) : base(connectionString)
        {

        }

        public IEnumerable<Game> GetGames()
        {
            IEnumerable<Game> _data = GetRecords<Game>(GamesQueries.GetAllGames, null);
            return _data;

        }

        public Game GetGameById(int ID)
        {
            Game _game = GetRecords<Game>(GamesQueries.GetGameByID, new { ID }).FirstOrDefault();

            // Get Game Questions
            // _game.Questions = get questions from DB.

        

            return _game;
        }

        public Game GetQuestionById(int ID)
        {
            throw new NotImplementedException();
        }

        public bool UpdateGame(Game game)
        {

            bool results = ExecuteAll(GamesQueries.UpdateGame, game);


            return results;
        }
    }
}
