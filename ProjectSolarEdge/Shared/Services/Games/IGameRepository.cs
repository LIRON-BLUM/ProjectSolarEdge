using System;
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

        Game GetGameById(int Id);

        bool UpdateGame(Game game);

        IEnumerable<GamesQuestions> GetGameQuestions();

        int AddGameToDB(Game game);


    }
}
