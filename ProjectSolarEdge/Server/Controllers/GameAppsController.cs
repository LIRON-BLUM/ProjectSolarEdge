﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectSolarEdge.Server.Configuration;
using ProjectSolarEdge.Shared.Entities;
using ProjectSolarEdge.Shared.Services.GameApp;

namespace ProjectSolarEdge.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameAppsController : ControllerBase
    {

        //properties
        private IGameAppRepository _gameAppRepository;

        public GameAppsController(IGameAppRepository _repo)
        {
            _gameAppRepository = _repo;
        }


        //[HttpGet]
        //[Route("GetCurrentScore")]
        //public IActionResult GetCurrentScore()
        //{
        //    IEnumerable<GameScore> gameScore = _gameAppRepository.getGameScore();

        //    return Ok(gameScore);
        //}


        [HttpGet]
        [Route("GetAllUsersGameRecord")]
        public IActionResult GetQuestions()
        {
            IEnumerable<UsersGameRecord> _data = _gameAppRepository.GetAllUsersGameRecord();

            return Ok(_data);
        }


        [HttpGet]
        [Route("GetAllUsersGameRecordByGameID/{gameId}")]
        public IActionResult UsersGameRecordByGameID(int gameId)
        {
            IEnumerable<UsersGameRecord> _data = _gameAppRepository.GetUsersGameRecordByGameId(gameId);

            if (_data == null)
            {
                return NotFound(new ApiResult
                {
                    Success = false
                });
            }

            return Ok(_data);

        }


        [HttpGet]
        [Route("AvailableQuestions/{GameID}/{UserID}")]
        public IActionResult AvailableQuestions(int GameID, int UserID)
        {
            IEnumerable<UsersGameRecord> _data = _gameAppRepository.AvailableQuestions(GameID, UserID);

            if (_data == null)
            {
                return NotFound(new ApiResult
                {
                    Success = false
                });
            }

            return Ok(_data);

        }


        [HttpGet]
        [Route("GetUserByID/{ID}")]
        public IActionResult GetPlayerByID(int ID)
        {
            UsersTable _player = _gameAppRepository.GetPlayerByID(ID);

            if (_player == null)
            {
                return NotFound(new ApiResult
                {
                    Success = false
                });
            }

            return Ok(_player);

        }





    }
}
