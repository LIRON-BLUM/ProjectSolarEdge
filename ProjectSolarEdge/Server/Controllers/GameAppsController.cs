using Microsoft.AspNetCore.Http;
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


        //[HttpGet]
        //[Route("GetAllUsersGameRecord")]
        //public IActionResult GetQuestions()
        //{
        //    IEnumerable<UsersGameRecord> _data = _gameAppRepository.GetAllUsersGameRecord();

        //    return Ok(_data);
        //}


        //[HttpGet]
        //[Route("GetAllUsersGameRecordByGameID/{gameId}")]
        //public IActionResult UsersGameRecordByGameID(int gameId)
        //{
        //    IEnumerable<UsersGameRecord> _data = _gameAppRepository.GetUsersGameRecordByGameId(gameId);

        //    if (_data == null)
        //    {
        //        return NotFound(new ApiResult
        //        {
        //            Success = false
        //        });
        //    }

        //    return Ok(_data);

        //}


     
        


        [HttpGet]
        [Route("GetUsersScore/{gameId}")]
        public IActionResult GetUserScore(int gameId)
        {
            IEnumerable<UserGameScore> _data = _gameAppRepository.GetUsersScore(gameId);

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
        [Route("GetGameUsersScoreByUserID/{GameID}/{UserID}")]
        public IActionResult GetGameUserScoreByUserID(int GameID, int UserID)
        {
            UserGameScore _data = _gameAppRepository.GetGameUserScoreByUserID(GameID, UserID);

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
            IEnumerable<Question> _data = _gameAppRepository.AvailableQuestions(GameID, UserID);

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

        [HttpPost]
        [Route("AddScoreElement")]
        public async Task<IActionResult> AddScoreElement(GameScore gameScore)
        {

            int _gameScore = _gameAppRepository.AddScoreElement(gameScore);


            if (_gameScore == null)
            {
                return NotFound(new ApiResult
                {
                    Success = false
                });
            }

            return Ok(_gameScore);

        }

        [HttpPut]
        [Route("UpdateScoreElement")]
        public IActionResult UpdateScoreElement(GameScore gameScore)
        {
            if (gameScore == null)
            {

                return BadRequest();
            }

            bool _gameScore = _gameAppRepository.UpdateScoreElement(gameScore);

            return Ok(_gameScore); //success
        }


        [HttpGet]
        [Route("GetAllUserGameScore/{GameID}/{UserID}")]
        public IActionResult GetAllUserGameScore(int GameID, int UserID)
        {
            IEnumerable<GameScore> _data = _gameAppRepository.GetAllUserGameScore(GameID, UserID);

            if (_data == null)
            {
                return NotFound(new ApiResult
                {
                    Success = false
                });
            }

            return Ok(_data);

        }


    }
}
