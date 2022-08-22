using ProjectSolarEdge.Server.Configuration;
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

        [HttpPut]
        [Route("UpdateGame/{Id}")]
        public IActionResult UpdateGame(Game game)
        {
            if (game == null)
            {

                return BadRequest();
            }

            bool _game = _GameRepository.UpdateGame(game);

            return Ok(_game); //success
        }


        

        [HttpPut]
        [Route("DeleteGame/{Id}")]
        public IActionResult DeleteGame(Game game)
        {
            if (game == null)
            {

                return BadRequest();
            }

            bool _game = _GameRepository.DeleteGame(game);

            return Ok(_game); //success
        }


        [HttpPost]
        [Route("InsertGame")]
        public async Task<IActionResult> AddGameToDB(Game game)
        {

            int _game = _GameRepository.AddGameToDB(game);


            if (_game == null)
            {
                return NotFound(new ApiResult
                {
                    Success = false
                });
            }

            return Ok(_game);

        }



        [HttpPost]
        [Route("InsertQuestionConnection")]
        public async Task<IActionResult> AddQuestionToConnection(GameQuestionsConnection gameQuestionsConnection)
        {

            int _newQuestiont = _GameRepository.AddQuestionToConnection(gameQuestionsConnection);


            if (_newQuestiont == null)
            {
                return NotFound(new ApiResult
                {
                    Success = false
                });
            }

            return Ok(_newQuestiont);

        }



        [HttpDelete]
        [Route("QuestionConnection/{Id}")]
        public IActionResult QuestionConnection(int Id)
        {
            if (Id == 0)
                return BadRequest();

            //var questionToDelete = _questionRepository.GetQuestionById(Id);
            if (Id == null)
                return NotFound();

            bool _QuesConnection = _GameRepository.DeleteQuestionConnction(Id);

            return Ok(_QuesConnection);//success
        }


        

        [HttpDelete]
        [Route("DeleteQuestionIDConnction/{QuestionID}/{GameID}")]
        public IActionResult DeleteQuestionIDConnction(int QuestionID, int gameID)
        {
            if (QuestionID == 0)
                return BadRequest();

            //var questionToDelete = _questionRepository.GetQuestionById(Id);
            if (QuestionID == null)
                return NotFound();

            bool _QuesConnection = _GameRepository.DeleteQuestionIDConnction(QuestionID, gameID);

            return Ok(_QuesConnection);//success
        }
    }


}



