﻿using ProjectSolarEdge.Server.Configuration;
using Microsoft.AspNetCore.Mvc;
using ProjectSolarEdge.Shared.Entities;
using ProjectSolarEdge.Shared.Services.Games;


namespace ProjectSolarEdge.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        //properties
        private IGameRepository _GameRepository;

        public GamesController(IGameRepository _repo)
        {
            _GameRepository = _repo;
        }

        [HttpGet]
        [Route("GetGames")]
        public IActionResult GetGames()
        {
            IEnumerable<Game> games = _GameRepository.GetGames();

            return Ok(games);
        }


        [HttpGet]
        [Route("Game/{Id}")]
        public IActionResult GetGameId(int Id)
        {
            Game _game = _GameRepository.GetGameById(Id);

            if (_game == null)
            {
                return NotFound(new ApiResult
                {
                    Success = false
                });
            }

            return Ok(_game);

        }
    }


}


