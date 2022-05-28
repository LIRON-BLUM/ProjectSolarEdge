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

        //[HttpGet]
        //[Route("GetSubjects")]
        //public IActionResult GetSubjects()
        //{
        //    IEnumerable<Subject> subjects = _questionRepository.getSubjects();

        //    return Ok(subjects);
        //}

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



        [HttpPost]
        [Route("Insert")]
        public async Task<IActionResult> AddQuestionToDB(Question newQuestion)
        {

            int _question = _questionRepository.AddQuestionToDB(newQuestion);


            if (_question == null)
            {
                return NotFound(new ApiResult
                {
                    Success = false
                });
            }

            return Ok(_question);

        }



        [HttpPut]
        [Route("Question/{Id}")]
        public IActionResult UpdateQuestion(Question question)
        {
            if (question == null)
            {

                return BadRequest();
            }

            bool _question = _questionRepository.UpdateQuestion(question);

            return Ok(_question); //success
        }

        [HttpPut]
        [Route("DeleteQuestion/{Id}")]
        public IActionResult DeleteQuestion(Question question)
        {
            if (question == null)
            {

                return BadRequest();
            }

            bool _question = _questionRepository.DeleteQuestion(question);

            return Ok(_question); //success
        }


        //[HttpDelete]
        //[Route("Question/{Id}")]
        //public IActionResult DeleteQuestion(int Id)
        //{
        //    if (Id == 0)
        //        return BadRequest();

        //    //var questionToDelete = _questionRepository.GetQuestionById(Id);
        //    if (Id == null)
        //        return NotFound();

        //    bool _question = _questionRepository.DeleteQuestion(Id);

        //    return Ok(_question);//success
        //}



        [HttpPost]
        [Route("InsertAns")]
        public async Task<IActionResult> AddAnswerToDB(QuestionAnswer answer)
        {
          

            bool _Answer = _questionRepository.AddAnswerToDB(answer);


            if (_Answer == null)
            {
                return NotFound(new ApiResult
                {
                    Success = false
                });
            }

            return Ok(_Answer);



        }



        [HttpPut]
        [Route("Answer/{Id}")]
        public IActionResult UpdateAnswer(QuestionAnswer answer)
        {
            if (answer == null)
            {

                return BadRequest();
            }

            bool _answer = _questionRepository.UpdateAnswer(answer);

            return Ok(_answer); //success
        }

        [HttpPut]
        [Route("DeleteAnswer/{Id}")]
        public IActionResult DeleteAnswer(QuestionAnswer answer)
        {
            if (answer == null)
            {

                return BadRequest();
            }

            bool _answer = _questionRepository.DeleteAnswer(answer);

            return Ok(_answer); //success
        }



        //[HttpDelete]
        //[Route("DeleteAnswer/{Id}")]
        //public IActionResult DeleteQuestionAnswer(int Id)
        //{
        //    if (Id == 0)
        //        return BadRequest();

        //    //var questionToDelete = _questionRepository.GetQuestionById(Id);
        //    if (Id == null)
        //        return NotFound();

        //    bool _question = _questionRepository.DeleteAnswer(Id);

        //    return Ok(_question);//success
        //}

    }
}


