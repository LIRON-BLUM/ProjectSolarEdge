using Microsoft.AspNetCore.Mvc;
using ProjectSolarEdge.Server.Configuration;
using ProjectSolarEdge.Shared.Entities;
using ProjectSolarEdge.Shared.Services.Questions;

namespace ProjectSolarEdge.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        //properties
        private IQuestionRepository _questionRepository;

        public QuestionsController(IQuestionRepository _repo)
        {
            _questionRepository = _repo;
        }

        [HttpGet]
        [Route("GetQuestions")]
        public IActionResult GetQuestions()
        {
            IEnumerable<Question> questions = _questionRepository.getQuestions();

            return Ok(questions);
        }

        [HttpGet]
        [Route("Question/{Id}")]
        public IActionResult GetQuestionById(int Id)
        {
            Question _question = _questionRepository.GetQuestionById(Id);

            if (_question == null)
            {
                return NotFound(new ApiResult
                {
                    Success = false
                });
            }

            return Ok(_question);

        }
    }
}
