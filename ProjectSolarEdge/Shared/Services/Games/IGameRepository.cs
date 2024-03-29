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

        Game GetGameById(int Id);


        bool UpdateGame(Game game);

        bool DeleteGame(Game game);

        IEnumerable<GamesQuestions> GetGameQuestions();

        int AddGameToDB(Game game);

        bool DeleteQuestionConnction(int ID);

        bool DeleteQuestionIDConnction(int QuestionID, int gameID);

        int AddQuestionToConnection(GameQuestionsConnection gameQuestionsConnection);

        bool UpdateGameQuestionsConnections(GameQuestionsConnection gameQuestionsConnection);

    }
}
